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
        //private static ConcurrentDictionary<string, IDbContext> dbContextDic = new ConcurrentDictionary<string, IDbContext>();
        //private IServiceProvider _provider;
        //public UnitOfWorkManager(IServiceProvider serviceProvider)
        //{
        //    _provider = serviceProvider;
        //}
        //public IDbContext GetDbContextByConnectonString(string connectionString)
        //{
        //    return dbContextDic[connectionString];
        //}
        //public IDbContext GetDbContextByEntity(Type type)
        //{
           
        //    //获取所有实体配置
        //    var service = _provider.GetService<IEntityConfigurationFinder>();
        //    var items = service.FinderAll().Select(x => Activator.CreateInstance(x) as IEntityTypeRegiest)
        //        .ToList();
        //    var dbType = items.FirstOrDefault(x => x.EntityType == type)?.DbContextType;
        //    var context = Activator.CreateInstance(dbType) as IDbContext;
        //    if (!dbContextDic.ContainsKey(context.ConnectionString))
        //    {
        //        dbContextDic.TryAdd(context.ConnectionString, context);
        //    }
        //    return context;    
        //}
    }
}
