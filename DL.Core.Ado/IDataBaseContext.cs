using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DL.Core.Ado
{
    public interface IDataBaseContext
    {
        

        /// <summary>
        /// 数据库类型
        /// </summary>
        DataBaseType Type { get; }

        /// <summary>
        /// 返回受影响的行数
        /// 用于增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>返回受影响的行数</returns>
        int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter);
        /// <summary>
        /// 返回受影响的行数
        /// 用于增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="type">执行类型</param>
        /// <param name="hashtable">键值对table</param>
        /// <returns>返回受影响的行数</returns>
        int ExecuteNonQuery(string sql, CommandType type, Hashtable hashtable);

        /// <summary>
        /// 返回受影响的行数
        /// 用于增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="type">执行类型</param>
        /// <param name="pairs">键值对字典</param>
        /// <returns>返回受影响的行数</returns>
        int ExecuteNonQuery(string sql, CommandType type, Dictionary<string,string>  pairs);

        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>数据表格</returns>
        DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter);
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
        DataTable GetPageDataTable(string tableName, int pageIndex, int pageSize, string orderByFiled,out int totalCount, string filterSql = null);


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
        DataSet GetPageDataSet(string tableName, int pageIndex, int pageSize, string orderByFiled, out int totalCount, string filterSql = null);
        /// <summary>
        /// 获取数据内存表格
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>内存数据表</returns>
        DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter);

        /// <summary>
        /// 获取数据对象
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>数据对象</returns>
        object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter);
    }

}
