using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.EfCore
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取当前工作单元
        /// </summary>
        IUnitOfWork CurrentUnitOfWork { get; }
        /// <summary>
        ///  根据实体获取当前工作单元
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IUnitOfWork GetUnitOfWorkByEntity(Type type);
        /// <summary>
        /// 根据实体获取数据库上下文
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IDbContext GetDbContextByEntity(Type type);
        /// <summary>
        /// 获取当前数据库上下文
        /// </summary>
        IDbContext CurrentDbContext { get; }
        /// <summary>
        /// 是否启用事务
        /// </summary>
        bool BeginTransaction { get; set; }
        /// <summary>
        /// 事务提交
        /// </summary>
        void CommitTransaction();
    }
}
