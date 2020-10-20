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
      //  IDbContext GetDbContextByEntity(Type type);
        /// <summary>
        /// 根据链接字符串获取工作上下文
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
       // IDbContext GetDbContextByConnectonString(string connectionString);

       
    }
}
