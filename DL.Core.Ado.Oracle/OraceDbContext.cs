using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using DL.Core.ulitity;
using System.Collections.Concurrent;
using DL.Core.ulitity.tools;
using System.Collections;

namespace DL.Core.Ado.Oracle
{
    public class OraceDbContext : DataBaseContext, IOraceDbContext
    {
       public static ConcurrentDictionary<string, IDbConnection> dbPars = new ConcurrentDictionary<string, IDbConnection>();
        public string CurrentConnectionString { get; private set; }
        private bool _begintransaction;
        public IDbConnection CurrentDbContext { get; private set; }
        private OracleConnection _OracleConnection;
        private OracleTransaction _OracleTranscation;
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
                    if (_OracleTranscation == null)
                        _OracleTranscation = _OracleConnection.BeginTransaction();
                }
                _begintransaction = value;
            }
        }

        public IDbTransaction CurrentDbTransaction => throw new NotImplementedException();

        public IDbConnection CreateDbConnection(string connectionString)
        {

            if (dbPars.ContainsKey(connectionString))
                return dbPars[connectionString];

            OracleConnection conn = new OracleConnection(connectionString);
           if (conn!=null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
                CurrentConnectionString = connectionString;
                _OracleConnection = conn;
            }
            CurrentDbContext = conn;
            return conn;
        }


        private int ExecuteSql(string sql, CommandType type, DbParameter[] parameter)
        {
            try
            {
                using (OracleCommand command = new OracleCommand(sql, _OracleConnection))
                {
                    if (parameter.Length > 0)
                    {
                        command.Parameters.AddRange(parameter);
                        command.BindByName = true;
                    } 
                    command.CommandType = type;
                    
                    if (BeginTransaction)
                       command.Transaction = _OracleTranscation;
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter)
        {
            return ExecuteSql(sql, type, parameter);
        }

        public override object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                using (OracleCommand command = new OracleCommand(sql, _OracleConnection))
                {
                    if (parameter.Length > 0)
                        command.Parameters.Add(parameter);
                    command.CommandType = type;
                    if(BeginTransaction)
                       command.Transaction = _OracleTranscation;
                    return command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                using (OracleCommand com = new OracleCommand(sql, _OracleConnection))
                {
                    if (parameter.Length > 0)
                        com.Parameters.Add(parameter);
                    com.CommandType = type;
                    if (BeginTransaction)
                        com.Transaction = _OracleTranscation;
                    DataSet ds = new DataSet();
                    using (OracleDataAdapter da = new OracleDataAdapter(com))
                    {
                        da.Fill(ds);
                        return ds;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                using (OracleCommand com = new OracleCommand(sql, _OracleConnection))
                {
                    if (parameter.Length > 0)
                        com.Parameters.Add(parameter);
                    com.CommandType = type;
                    if (BeginTransaction)
                        com.Transaction = _OracleTranscation;
                    DataTable dt = new DataTable();
                    using (OracleDataAdapter da = new OracleDataAdapter(com))
                    {
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override DataSet GetPageDataSet(string tableName, int pageIndex, int pageSize, string orderByFiled, out int totalCount, string filterSql = null)
        {
            if (filterSql != null)
            {
                totalCount = 0;
                int start = (pageIndex - 1) * pageSize + 1;
                int end = pageIndex * pageSize;
                //统计数量
                var totalSql = string.Format("SELECT COUNT(1) as totalCount FROM {0} WHERE 1=1 {1}", tableName, filterSql);
                totalCount = ExecuteScalar(totalSql, CommandType.Text).ToInt32();
                //分页执行
                var exesql = string.Format("select rn,s.* from  (select rownum as rn,t.* from {0} {3} t where rownum<={1}) s where rn>={2}", tableName, end, start,filterSql);
                return GetDataSet(exesql, CommandType.Text);
            } else
            {
                totalCount = 0;
                int start = (pageIndex - 1) * pageSize + 1;
                int end = pageIndex * pageSize;
                //统计数量
                var totalSql = string.Format("SELECT COUNT(1) as totalCount FROM {0} WHERE 1=1", tableName);
                totalCount = ExecuteScalar(totalSql, CommandType.Text).ToInt32();
                //分页执行
                var exesql = string.Format("select rn,s.* from  (select rownum as rn,t.* from {0}  t where rownum<={1}) s where rn>={2}", tableName, end, start);
                return GetDataSet(exesql, CommandType.Text);
            }
           
        }

        public override DataTable GetPageDataTable(string tableName, int pageIndex, int pageSize, string orderByFiled, out int totalCount, string filterSql = null)
        {
            if (filterSql != null)
            {
                totalCount = 0;
                int start = (pageIndex - 1) * pageSize + 1;
                int end = pageIndex * pageSize;
                //统计数量
                var totalSql = string.Format("SELECT COUNT(1) as totalCount FROM {0} WHERE 1=1 {1}", tableName, filterSql);
                totalCount = ExecuteScalar(totalSql, CommandType.Text).ToInt32();
                //分页执行
                var exesql = string.Format("select rn,s.* from  (select rownum as rn,t.* from {0} {3} t where rownum<={1}) s where rn>={2}", tableName, end, start, filterSql);
                return GetDataTable(exesql, CommandType.Text);
            }
            else
            {
                totalCount = 0;
                int start = (pageIndex - 1) * pageSize + 1;
                int end = pageIndex * pageSize;
                //统计数量
                var totalSql = string.Format("SELECT COUNT(1) as totalCount FROM {0} WHERE 1=1", tableName);
                totalCount = ExecuteScalar(totalSql, CommandType.Text).ToInt32();
                //分页执行
                var exesql = string.Format("select rn,s.* from  (select rownum as rn,t.* from {0}  t where rownum<={1}) s where rn>={2}", tableName, end, start);
                return GetDataTable(exesql, CommandType.Text);
            }
        }

        public bool SaveTransactionChange()
        {
            bool result = false;
            try
            {
                if (BeginTransaction && _OracleTranscation!=null)
                {
                    
                    _OracleTranscation.Commit();
                    result = true;
                }
            }
            catch (Exception)
            {
                _OracleTranscation.Rollback();
                result = false;
            }
            finally
            {
                if (_OracleTranscation!=null)
                {
                    _OracleTranscation.Dispose();
                    _OracleTranscation = null;
                }    
            }
            return result;
        }

        public override int ExecuteNonQuery(string sql, CommandType type, Hashtable hashtable)
        {

            List<OracleParameter> list = new List<OracleParameter>();
            foreach (var item in hashtable.Keys)
            {
                var parmars = item.ToString();
                var itemValue = hashtable[item];
                list.Add(new OracleParameter(parmars, itemValue));
            }
            var arrys = list.ToArray();
            return ExecuteNonQuery(sql, type, arrys);
        }

        public override int ExecuteNonQuery(string sql, CommandType type, Dictionary<string, string> pairs)
        {
            List<OracleParameter> list = new List<OracleParameter>();
            foreach (var item in pairs.Keys)
            {
                var parmars = item.ToString();
                var itemValue = pairs[item];
                list.Add(new OracleParameter(parmars, itemValue));
            }
            var arrys = list.ToArray();
            return ExecuteNonQuery(sql, type, arrys);
        }
    }
}
