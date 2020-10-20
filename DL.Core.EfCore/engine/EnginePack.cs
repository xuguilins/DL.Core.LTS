using DL.Core.EfCore.finderPacks;
using DL.Core.EfCore.packBase;
using DL.Core.ulitity.configer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.EfCore.engine
{
    public static class EnginePack
    {
        private static IServiceProvider ServiceProvider; 
        public static IServiceCollection  AddEnginePack<TDContext>(this IServiceCollection  services) where TDContext:DbContext
        {
            services.AddDbContext<TDContext>();
            var type = typeof(TDContext);
           // AutoMigration(type);
            var packFinder = new PackModuleFinder();
            var packTypes= packFinder.FinderAll();
            foreach (var item in packTypes)
            {
                var pack = Activator.CreateInstance(item) as PackModule;
                pack.AddService(services);
            }
            var provider = services.BuildServiceProvider();
            ServiceProvider = provider;
            AutoMigration(type);
            ServiceLocator.Instace.SetProvider(provider);
            return services;
        } 
        private static void AutoMigration(Type dbContext)
        {
            var config = ConfigManager.Build.DbConfig;
            if (config.AutoEFMigrationEnable)
            {
                var contexts = ServiceProvider.GetServices(dbContext).ToList();  //ServiceLocator.Instace.ServiceProvider.GetServices(dbContext).ToList();
                foreach (var item in contexts)
                {
                    var context = item as DbContext;
                    try
                    {
                        if (context.Database.GetPendingMigrations().Any())
                        {
                            context.Database.Migrate();
                           // return "迁移完毕";
                        }
                        else
                        {
                           // return "未检测到含有启动迁移的文件或数据实体未发生任何改变,请尝试运行 Add-Migration 指令";
                        }
                    }
                    catch (Exception ex)
                    {
                      
                        throw new Exception($"自动迁移发生异常，异常原因：{ex.Message}");
                    }


                ....5366
                        
                        
                        66666666}

            }

        }
     
    }
}
