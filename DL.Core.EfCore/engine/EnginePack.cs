﻿using DL.Core.EfCore.finderPacks;
using DL.Core.EfCore.packBase;
using DL.Core.ulitity.configer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Core.ulitity.tools;
using DL.Core.ulitity.log;
using System.Diagnostics;

namespace DL.Core.EfCore.engine
{
    public static class EnginePack
    {
        private static IServiceProvider ServiceProvider;
        private static ILogger logger = LogManager.GetLogger();
        private static  bool AutoMigrationEable { get; set; }
        public static IServiceCollection  AddEnginePack<TDContext>(this IServiceCollection  services) where TDContext:DbContext
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"****框架初始化开始**** [{DateTime.Now}]\r\n ");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            services.AddDbContext<TDContext>();
            var type = typeof(TDContext);
            var packFinder = new PackModuleFinder();
            sb.Append($"****准备查找Pack模块包**** [{DateTime.Now}]\r\n ");
            var packTypes= packFinder.FinderAll();
            foreach (var item in packTypes)
            {
                var pack = Activator.CreateInstance(item) as PackModule;
                pack.AddService(services);
            }

            sb.Append($"****模块包注入完毕**** [{DateTime.Now}]\r\n ");
            var provider = services.BuildServiceProvider();
            ServiceProvider = provider;
            sb.Append($"****开始验证是否进行数据库迁移**** [{DateTime.Now}]\r\n ");
            AutoMigration(type);
            ServiceLocator.Instace.SetProvider(provider);
            sb.Append($"****框架初始化完毕**** [{DateTime.Now}]\r\n ");
            sw.Stop();
            sb.Append($"****以上操作耗时**** [{sw.ElapsedMilliseconds}ms]\r\n ");
            logger.Info(sb.ToString());
            return services;
        } 
        /// <summary>
        ///  是否启用自动迁移
        /// </summary>
        /// <param name="enable">默认启用：true</param>
        public static void EnableAutoMigration(this IServiceCollection services, bool enable= true)
        {
            AutoMigrationEable = enable;
        }
        private static void AutoMigration(Type dbContext)
        {
            var config = ConfigManager.Build.DbConfig;
            if (config.AutoEFMigrationEnable || AutoMigrationEable)
            {
                var contexts = ServiceProvider.GetServices(dbContext).ToList(); 
                foreach (var item in contexts)
                {
                    var context = item as DbContext;
                    try
                    {
                        if (context.Database.GetPendingMigrations().Any())
                        {
                            context.Database.Migrate();     
                        }
                    }
                    catch (Exception ex)
                    {  
                        throw new Exception($"自动迁移发生异常，异常原因：{ex.Message}");
                    }
                }
            }
        }
     
        /// <summary>
        /// 加载指定模块
        /// </summary>
        /// <typeparam name="TPack"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UserPack<TPack> (this IServiceCollection services) where TPack:PackModule
        {
            var type = typeof(TPack);
            var instance = Activator.CreateInstance(type) as PackModule;
            var serviceList = instance.AddService(services);
            return serviceList;
        }
    }
}
