using DL.Core.EfCore.finderPacks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.EfCore
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private IServiceProvider _serviceProvider;
        private ScopedDictory dic;
        public UnitOfWorkManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            dic = _serviceProvider.GetService<ScopedDictory>();

        }
        /// <summary>
        /// 根据实体类型获取工作上下文
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
       public IUnitOfWork GetUnitOfWorkByEntity(Type type)
        {
            var key = $"dbentityunit-{type.Name}";
            return CreateUniOfWork(key);
        }
        /// <summary>
        /// 根据链接字符串获取工作上下文
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
       public  IUnitOfWork GetUnitOfWorkConnectonString(string connectionString)
        {
            var key = $"dbunit-{connectionString}";
            return CreateUniOfWork(key);
        }
        /// <summary>
        /// 根据指定上下文获取工作单元
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
       public IUnitOfWork GetUnitOfWorkByDbContext(Type dbType)
        {
            var key = $"dbcontextName-{dbType.Name}";
            return CreateUniOfWork(key);
        }
        private IUnitOfWork CreateUniOfWork(string key)
        {
            return dic[key] as IUnitOfWork;
        }
    }
}
