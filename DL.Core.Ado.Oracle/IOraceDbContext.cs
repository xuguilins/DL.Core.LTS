using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DL.Core.Ado.Oracle
{
    public interface IOraceDbContext:IDataBaseContext
    {
        /// <summary>
        /// 创建数据库链接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IDbConnection CreateDbConnection(string connectionString);

        /// <summary>
        /// 当前数据库对象链接字符串
        /// </summary>
        string CurrentConnectionString { get; }
        /// <summary>
        /// 获取当前数据库链接对象
        /// </summary>
        IDbConnection CurrentDbContext { get; }
        /// <summary>
        /// 获取或设置事务
        /// </summary>
        bool BeginTransaction { get; set; }
        /// <summary>
        /// 获取当前事务对象
        /// </summary>
        IDbTransaction CurrentDbTransaction { get; }
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        bool SaveTransactionChange();
    }
}
