using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.EfCore
{
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 根据实体类型获取工作上下文
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IUnitOfWork GetUnitOfWorkByEntity(Type type);
        /// <summary>
        /// 根据链接字符串获取工作上下文
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IUnitOfWork GetUnitOfWorkConnectonString(string connectionString);
        /// <summary>
        /// 根据指定上下文获取工作单元
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        IUnitOfWork GetUnitOfWorkByDbContext(Type dbType);
    }
}
