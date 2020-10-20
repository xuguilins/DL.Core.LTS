using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using DL.Core.Ado;
namespace DL.Core.Ado.SqlServer
{
    public interface ISqlServerDbContext:IDataBaseContext
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
        IDbConnection CurrentDbContext { get;  }
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
        /// <summary>
        /// 实体写入
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// 批量写入实体数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        int InsertItems<TEntity>(List<TEntity> entities) where TEntity : class;
        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        TEntity SetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity:class,new();
        /// <summary>
        /// 查询指定实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        List<TEntity> Set<TEntity>() where TEntity : class, new();
    }
}
