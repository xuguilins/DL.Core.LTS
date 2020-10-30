using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Linq;
using DL.Core.ulitity.table;
using DL.Core.ulitity.attubites;

namespace DL.Core.Ado.MySql
{
    public class MySqlDbContext:DataBaseContext, IMySqlDbContext
    {
       
        private MySqlConnection _MySqlConnection;
        private MySqlTransaction _MySqlTransaction;
        private bool _begintransaction = false;
        private static readonly ConcurrentDictionary<string, MySqlConnection> conPairs = new ConcurrentDictionary<string, MySqlConnection>();

        public override DataBaseType Type => DataBaseType.SqlServer;
        public string CurrentConnectionString { get; private set; }
        public IDbConnection CurrentDbContext { get; private set; }
        public IDbConnection CreateDbConnection(string connectionString)
        {
            try
            {
                if (conPairs.ContainsKey(connectionString))
                {
                    CurrentDbContext = conPairs[connectionString];
                    return conPairs[connectionString];
                }
                else
                {
                    _MySqlConnection = new MySqlConnection(connectionString);
                    if (_MySqlConnection != null && _MySqlConnection.State == ConnectionState.Closed)
                    {
                        _MySqlConnection.Open();
                        CurrentConnectionString = connectionString;

                    }
                    CurrentDbContext = _MySqlConnection;
                    return _MySqlConnection;
                }
            }
            catch (Exception ex)
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
                    if (_MySqlTransaction == null)
                        _MySqlTransaction = _MySqlConnection.BeginTransaction();
                }
                _begintransaction = value;
            }

        }
        public IDbTransaction CurrentDbTransaction => _MySqlTransaction;
        private void ValidateConnection()
        {
            if (_MySqlConnection == null || _MySqlConnection.State == ConnectionState.Closed)
                throw new Exception($"无效的数据库链接，请检查数据库链接是否已创建");
        }
        protected private int ExecuteSql(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                
                ValidateConnection();
                using (MySqlCommand com = new MySqlCommand(sql, _MySqlConnection, _MySqlTransaction))
                {
                    com.CommandText = sql;
                    com.Parameters.AddRange(parameter);
                    com.CommandType = type;
                    return com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public override int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter)
        {
            return ExecuteSql(sql, type, parameter);
        }
        public TEntity SetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, new()
        {
            try
            {

                var express = expression.Body as BinaryExpression;
                string sqlWhere = string.Empty;
                string rightValue = string.Empty;
                string leftValue = string.Empty;
                if (express != null)
                {
                    var right = express.Right as ConstantExpression;
                    var left = express.Left as MemberExpression; //as express;
                    if (right == null)
                        throw new Exception("表达式异常，无法解析，请检查表达式右侧的值");
                    rightValue = right.Value.ToString();
                    if (left == null)
                        throw new Exception("表达式异常，无法解析，请检查表达式左侧的式子");
                    leftValue = left.Member.Name;
                }
                var ex = expression.Body;
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<TEntity> Set<TEntity>() where TEntity : class, new()
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
                throw new Exception("请检查事务是否开启");
            if (_MySqlTransaction == null)
                throw new Exception("无效的事务对象");
            try
            {
                _MySqlTransaction.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                _MySqlTransaction.Rollback();
            }
            finally
            {
                if (_MySqlTransaction != null)
                {
                    _MySqlTransaction.Dispose();
                    _MySqlTransaction = null;
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
                var itemCodes = string.Join(",", props.Select(x => x.Name));
                var tableName = GetTableName(type);
                StringBuilder sb = new StringBuilder();
                sb.Append($"INSERT INTO  {tableName}({itemCodes})VALUES(");
                string strValues = string.Empty;
                foreach (var item in props)
                {

                    var value = item.GetValue(entity, null);
                    if (value == null || value == DBNull.Value)
                        value = "";
                    strValues += $"'{value}',";
                }
                sb.Append(strValues.Substring(0, strValues.Length - 1) + ")");
                var executeSql = sb.ToString();
                return ExecuteNonQuery(executeSql, CommandType.Text);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int InsertItems<TEntity>(List<TEntity> entities) where TEntity : class
        {
            try
            {
                if (entities == null)
                    throw new Exception($"请传入有效的实体对象");
                if (!entities.Any())
                    throw new Exception("请传入有效的对象数据");
                var entity = entities.FirstOrDefault();
                var type = entity.GetType();
                var props = type.GetProperties();
                var itemCodes = string.Join(",", props.Select(x => x.Name));
                var tableName = GetTableName(type);
                StringBuilder sb = new StringBuilder();
                foreach (var item in entities)
                {
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
                }
                var executeSql = sb.ToString();
                return ExecuteSql(executeSql, CommandType.Text);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public override DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                ValidateConnection();
                using (MySqlCommand com = new MySqlCommand(sql, _MySqlConnection))
                {
                    com.CommandType = type;
                    com.Parameters.AddRange(parameter);
                    DataTable dt = new DataTable();
                    
                    using (MySqlDataAdapter da = new MySqlDataAdapter(com))
                    {
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public override DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                ValidateConnection();
                using (MySqlCommand com = new MySqlCommand(sql, _MySqlConnection))
                {
                    com.CommandType = type;
                    com.Parameters.AddRange(parameter);
                    DataSet ds = new DataSet();
                    using (MySqlDataAdapter da = new MySqlDataAdapter(com))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                ValidateConnection();
                using (MySqlCommand com = new MySqlCommand(sql, _MySqlConnection, _MySqlTransaction))
                {
                    com.CommandText = sql;
                    com.Parameters.AddRange(parameter);
                    com.CommandType = type;
                    return com.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string GetTableName(Type type)
        {
            var attbuite = type.GetCustomAttributes(false);
            if (attbuite != null && attbuite.Length > 0)
            {
                var tableAttbuite = attbuite[0] as TableAttubite;
                return tableAttbuite.TableName;
            }
            else
            {
                return type.Name;
            }


        }

        public override DataTable GetPageDataTable(string tableName, int pageIndex, int pageSize, string orderByFiled, out int totalCount, string filterSql = null)
        {
            throw new NotImplementedException();
        }
        public override DataSet GetPageDataSet(string tableName, int pageIndex, int pageSize, string orderByFiled, out int totalCount, string filterSql = null)
        {
            throw new NotImplementedException();
        }
    }
}
