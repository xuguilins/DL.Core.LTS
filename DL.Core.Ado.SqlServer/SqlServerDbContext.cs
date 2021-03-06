﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DL.Core.Ado;
using DL.Core.Ado.SqlServer;
using DL.Core.Ado.SqlServer.finders;
using DL.Core.ulitity.attubites;
using DL.Core.ulitity.configer;
using DL.Core.ulitity.log;
using DL.Core.ulitity.table;
using DL.Core.ulitity.tools;
namespace DL.Core.Ado.SqlServer
{
    public class SqlServerDbContext : DataBaseContext, ISqlServerDbContext
    {

        private SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;
        private bool _begintransaction = false;
        private static readonly ConcurrentDictionary<string, SqlConnection> conPairs = new ConcurrentDictionary<string, SqlConnection>();
        private ILogger logger = LogManager.GetLogger<SqlServerDbContext>();
        public override DataBaseType Type => DataBaseType.SqlServer;
        public string CurrentConnectionString { get; private set; }
        public IDbConnection CurrentDbContext { get; private set; }
        public IDbConnection CreateDbConnection(string connectionString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new SqlServerException($"无效的数据库链接字符串");
                if (conPairs.ContainsKey(connectionString))
                {
                    CurrentDbContext = conPairs[connectionString];
                    return conPairs[connectionString];
                } else
                {
                    _sqlConnection = new SqlConnection(connectionString);
                    if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Closed)
                    {
                        _sqlConnection.Open();
                        CurrentConnectionString = connectionString;

                    }
                    CurrentDbContext = _sqlConnection;
                    return _sqlConnection;
                } 
            }
            catch (SqlServerException ex)
            {
                throw ex;
            }
        }
        public bool BeginTransaction
        {
            get
            {
                return _begintransaction;
            }
            set
            {
                if (value)
                {
                    if (_sqlTransaction == null)
                        _sqlTransaction = _sqlConnection.BeginTransaction();
                }
                _begintransaction = value;
            }

        }
        public IDbTransaction CurrentDbTransaction => _sqlTransaction;
        private void ValidateConnection()
        {
            if (_sqlConnection == null || _sqlConnection.State == ConnectionState.Closed)
                throw new SqlServerException($"无效的数据库链接，请检查数据库链接是否已创建");
        }
        public void AutoInitDataBaseTable()
        {
            var config = ConfigManager.Build.DbConfig;
            if (config != null && config.AutoAdoNetMiagraionEnable)
            {
                IAdoEntityFinder finder = new AdoEntityFinder();
                var items = finder.FinderAll();
                StringBuilder sb = new StringBuilder();
                foreach (var entity in items)
                {
                    var type = entity.GetType();
                    var tableName = GetTableName(type);
                    sb.Append($"CREATE TABLE {tableName} ");
                    sb.Append("(");
                    var props = type.GetProperties();
                    foreach (var item in props)
                    {
                        var length = CreateHelper.GetLength(item).ToString();
                        var typeName = CreateHelper.ParsePropType(item.PropertyType.Name, length);
                        if (item.Name.ToLower() == "id")
                        {
                            sb.Append($"{item.Name} {typeName} primary key not null, ");
                        } else
                        {
                            sb.Append($"{item.Name} {typeName},");
                        }
                    }
                    sb.Append(")");


                }
                ValidateConnection();
                ExecuteSql(sb.ToString(), CommandType.Text);
            }
        }
        protected private int ExecuteSql(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                ValidateConnection();
                using (SqlCommand com = new SqlCommand(sql, _sqlConnection))
                {
                    com.CommandText = sql;
                    if (parameter.Length > 0)
                        com.Parameters.AddRange(parameter);
                    if (BeginTransaction)
                        com.Transaction = _sqlTransaction;
                    com.CommandType = type;
                    return com.ExecuteNonQuery();
                }
            }
            catch (SqlServerException ex)
            {

                throw ex;
            }

        }

        public override int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter)
        {
            return ExecuteSql(sql, type, parameter);
        }
        public TEntity SetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class,new()
        {
            try
            {
              
                var express = expression.Body as BinaryExpression;
                string sqlWhere = string.Empty;
                string rightValue = string.Empty;
                string leftValue = string.Empty;
                if (express!=null)
                {
                    var right = express.Right as ConstantExpression;
                    var left = express.Left  as MemberExpression; //as express;
                    if (right == null)
                        throw new SqlServerException("表达式异常，无法解析，请检查表达式右侧的值");
                   rightValue = right.Value.ToString();                                                       
                    if (left==null)
                        throw new SqlServerException("表达式异常，无法解析，请检查表达式左侧的式子");
                    leftValue = left.Member.Name;
                } else
                {
                    var equerPressions = expression.Body as MethodCallExpression;
                    var memberItem = equerPressions.Object as MemberExpression;
                    if (memberItem != null)
                        leftValue = memberItem.Member.Name;
                    if (leftValue == null)
                        throw new Exception("表达式异常，无法解析，请检查表达式左侧的式子");
                    if (equerPressions != null)
                    {
                        var parms = equerPressions.Arguments?.FirstOrDefault();
                        if (parms != null)
                        {
                            var consantItem = parms as ConstantExpression;
                            if (consantItem != null)
                                rightValue = consantItem.Value.ToString();
                            if (rightValue == null)
                                throw new Exception("表达式异常，无法解析，请检查表达式右侧的值");
                        }
                    }
                }
                var model = new TEntity();
                var type = model.GetType();
                var props = type.GetProperties();
                var tableName = GetTableName(type);
                string itemCodes = string.Join(",", props.Select(x => x.Name));
                string executeSql = $"SELECT TOP 1 {itemCodes} from {tableName} WHERE  {leftValue}='{rightValue}'";
                var table = GetDataTable(executeSql, CommandType.Text);
                var entity = table.ToObject<TEntity>();
                return entity;
            }
            catch (SqlServerException ex)
            {

                throw ex;
            }
        }

        public List<TEntity> Set<TEntity>() where TEntity:class,new()
        {
            var model = new TEntity();
            var type = model.GetType();
            var props = type.GetProperties();
            var itemCodes = string.Join(",", props.Select(x => x.Name));
            var tableName = GetTableName(type);
            var executeSql = $"SELECT {itemCodes} FROM {tableName} ";
            var table = GetDataTable(executeSql, CommandType.Text);
            return table.ToObjectList<TEntity>();
        }
        public bool SaveTransactionChange()
        {
            bool result = false;
            if (!BeginTransaction)
                throw new SqlServerException("请检查事务是否开启");
            if (_sqlTransaction == null)
                throw new SqlServerException("无效的事务对象");
            try
            {
                _sqlTransaction.Commit();
                result = true;
            }
            catch (SqlServerException ex)
            {
                _sqlTransaction.Rollback();  
            }
            finally
            {
                if (_sqlTransaction != null)
                {
                    _sqlTransaction.Dispose();
                    _sqlTransaction = null;
                }
            }

            return result;
        }
        public int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                var type = entity.GetType();         
                var props = type.GetProperties();
                var tableName = GetTableName(type);
                StringBuilder sb = new StringBuilder();
                string strValues = string.Empty;
                string itemCodes = string.Empty;
                string paramesCodes = string.Empty;
                Hashtable hs = new Hashtable();
                foreach (var item in props)
                {
                    var isIgnore = GetColumeIgnore(item);
                    if (!isIgnore)
                    {
                        itemCodes += item.Name + ",";
                        paramesCodes += "@"+item.Name + ",";
                        var value = item.GetValue(entity, null);
                        hs.Add(item.Name, value);
                    }
                }
                itemCodes = itemCodes.ExpenSubstr();
                paramesCodes = paramesCodes.ExpenSubstr();
                var sql = $"INSERT INTO {tableName} ({itemCodes})VALUES({paramesCodes})";
                return ExecuteNonQuery(sql, CommandType.Text,hs);
            }
            catch (SqlServerException ex)
            {

                throw ex;
            }
      
        }
        public int InsertItems<TEntity>(List<TEntity> entities) where TEntity : class
        {
            try
            {
                if (entities == null)
                    throw new SqlServerException($"请传入有效的实体对象");
                if (!entities.Any())
                    throw new SqlServerException("请传入有效的对象数据");
                var entity = entities.FirstOrDefault();
                var type = entity.GetType();
                var props = type.GetProperties();
                var itemCodes = string.Join(",", props.Select(x => x.Name));
                var tableName = GetTableName(type);
                int AllCount = entities.Count;
                int forCount = 500;
           
                    //批次提交
                    int startCount = 0;
                    //计算可以分多少次
                    int PageCount = Convert.ToInt32(Math.Ceiling((double)AllCount / 500));
                    do
                    {
                        if (AllCount < 500)
                            forCount = AllCount;
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < forCount; j++)
                        {
                            var item = entities[j];
                            string strValues = string.Empty;
                            foreach (var pos in props)
                            {
                                var value = pos.GetValue(item, null);
                                if (value == null || value == DBNull.Value)
                                    value = "";
                                strValues += $"'{value}',";

                            }
                            var codeValue = strValues.Substring(0, strValues.Length - 1);
                            sb.Append($"INSERT INTO  {tableName}({itemCodes})VALUES({codeValue});");
                            startCount++;
                        }
                        entities.RemoveRange(0, forCount);
                        AllCount = entities.Count;
                         var executeSql = sb.ToString();
                    if (string.IsNullOrWhiteSpace(executeSql))
                        break;
                       ExecuteSql(executeSql, CommandType.Text);
                    } while (PageCount-- > 0);
                   return 1;
            }
            catch (SqlServerException ex)
            {
                throw;
            }
          

        }
        public override DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                ValidateConnection();
                using (SqlCommand com = new SqlCommand(sql, _sqlConnection))
                {
                    com.CommandType = type;
                    if (parameter.Length > 0)
                        com.Parameters.AddRange(parameter);
                    if (BeginTransaction)
                        com.Transaction = _sqlTransaction;
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                      
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (SqlServerException ex)
            {

                throw ex;
            }
            
        }
        public override DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                ValidateConnection();
                using (SqlCommand com = new SqlCommand(sql, _sqlConnection))
                {
                    com.CommandType = type;
                    if (parameter.Length > 0)
                        com.Parameters.AddRange(parameter);
                    if (BeginTransaction)
                        com.Transaction = _sqlTransaction;
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter(com))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (SqlServerException ex)
            {

                throw ex;
            }
        }
        public override object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                ValidateConnection();
                using (SqlCommand com = new SqlCommand(sql, _sqlConnection, _sqlTransaction))
                {
                    com.CommandText = sql;
                    if (parameter.Length > 0)
                        com.Parameters.AddRange(parameter);
                    if (BeginTransaction)
                        com.Transaction = _sqlTransaction;
                    com.CommandType = type;
                    return com.ExecuteScalar();
                }
            }
            catch (SqlServerException ex)
            {

                throw ex;
            }

        }

        private string GetTableName(Type type)
        {
            var attbuite = type.GetCustomAttributes(false);
            if (attbuite!=null && attbuite.Length > 0) {
                var tableAttbuite = attbuite[0] as TableAttubite;
                return tableAttbuite.TableName;
            } else
            {
                return type.Name;
            }


        }
        private bool GetColumeIgnore(PropertyInfo type)
        {
            var attbuite = type.GetCustomAttributes(false);
            if (attbuite != null && attbuite.Length > 0)
            {
                var tableAttbuite = attbuite[0] as IgnoerColume;
                return tableAttbuite.Ignore;
            }
            else
            {
                return false;
            }
        }

        public override DataTable GetPageDataTable(string tableName, int pageIndex, int pageSize, string orderByFiled, out int totalCount, string filterSql = null)
        {
            totalCount = 0;
            //统计记录数
            if (!string.IsNullOrWhiteSpace(filterSql))
            {
                //统计数量
                var totalSql = $"SELECT COUNT(1) AS TotalCount FROM {tableName} where  1=1 {filterSql}";
                totalCount = Convert.ToInt32(ExecuteScalar(totalSql, CommandType.Text));
                int start = (pageIndex - 1) * pageSize + 1;
                int end = pageIndex * pageSize;
                var sql = $"SELECT * FROM (select  ROW_NUMBER() OVER(ORDER BY {orderByFiled}) AS num ,   * FROM {tableName} where 1=1 {filterSql}  ) AS T WHERE T.num>={start} and T.num<={end}";
                return GetDataTable(sql, CommandType.Text);

            } else
            {
                //统计数量
                var totalSql = $"SELECT COUNT(1) AS TotalCount FROM {tableName}";
                totalCount = Convert.ToInt32(ExecuteScalar(totalSql, CommandType.Text));
                //计算索引
                int start = (pageIndex - 1) * pageSize + 1;
                int end = pageIndex * pageSize;
                var sql = $"SELECT * FROM (select  ROW_NUMBER() OVER(ORDER BY {orderByFiled}) AS num ,   * FROM {tableName} where 1=1   ) AS T WHERE T.num>={start} and T.num<={end}";
                return GetDataTable(sql, CommandType.Text);
            }
        }

        /// <summary>
        /// 分页获取数据表格
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pageIndex">当前页码值</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="orderByFiled">排序号</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="filterSql">查询语句，'AND XXXX'</param>
        /// <returns></returns>
       public override DataSet GetPageDataSet(string tableName, int pageIndex, int pageSize, string orderByFiled, out int totalCount, string filterSql = null)
       {
            totalCount = 0;
            //统计记录数
            if (!string.IsNullOrWhiteSpace(filterSql))
            {
                //统计数量
                var totalSql = $"SELECT COUNT(1) AS TotalCount FROM {tableName} where  1=1 {filterSql}";
                totalCount = Convert.ToInt32(ExecuteScalar(totalSql, CommandType.Text));
                int start = (pageIndex - 1) * pageSize + 1;
                int end = pageIndex * pageSize;
                var sql = $"SELECT * FROM (select  ROW_NUMBER() OVER(ORDER BY {orderByFiled}) AS num ,   * FROM {tableName} where 1=1 {filterSql}  ) AS T WHERE T.num>={start} and T.num<={end}";
                return GetDataSet(sql, CommandType.Text);

            }
            else
            {
                //统计数量
                var totalSql = $"SELECT COUNT(1) AS TotalCount FROM {tableName}";
                totalCount = Convert.ToInt32(ExecuteScalar(totalSql, CommandType.Text));
                //计算索引
                int start = (pageIndex - 1) * pageSize + 1;
                int end = pageIndex * pageSize;
                var sql = $"SELECT * FROM (select  ROW_NUMBER() OVER(ORDER BY {orderByFiled}) AS num ,   * FROM {tableName} where 1=1   ) AS T WHERE T.num>={start} and T.num<={end}";
                return GetDataSet(sql, CommandType.Text);
            }
        }

        public override int ExecuteNonQuery(string sql, CommandType type, Hashtable hashtable)
        {
          
            List<SqlParameter> list = new List<SqlParameter>();
            foreach (var item in hashtable.Keys)
            {
                var parmars = item.ToString();
                var itemValue = hashtable[item];
                list.Add(new SqlParameter(parmars, itemValue));
            }
            var arrys = list.ToArray();
            return ExecuteNonQuery(sql, type, arrys);
        }

        public override int ExecuteNonQuery(string sql, CommandType type, Dictionary<string, string> pairs)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            foreach (var item in pairs.Keys)
            {
                var parmars = item.ToString();
                var itemValue = pairs[item];
                list.Add(new SqlParameter(parmars, itemValue));
            }
            var arrys = list.ToArray();
            return ExecuteNonQuery(sql, type, arrys);
        }
    }
}