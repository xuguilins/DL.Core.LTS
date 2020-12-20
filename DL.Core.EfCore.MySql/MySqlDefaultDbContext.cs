using DL.Core.EfCore.finderPacks;
using DL.Core.ulitity.configer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DL.Core.EfCore.MySql
{

    public class MySqlDefaultDbContext<MySqlDbConntext> : DbContextBase<MySqlDbConntext> where MySqlDbConntext : DbContext
    {

        /// <summary>
        /// 连接字符串注册
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionString = ConfigManager.Build.ConnectionString.MySqlDefault;
            Console.WriteLine($"MYSQL:{ConnectionString}");
            optionsBuilder.UseMySql(ConnectionString);
        }
        /// <summary>
        /// 实体注册
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEntityBaseFinder entityFinder = new EntityBaseFinder();
            var entityItems = entityFinder.FinderAll();
            foreach (var enttiy in entityItems)
            {
                modelBuilder.Entity(enttiy);
            }
            //实体配置注册
            IEntityConfigurationFinder configService = new EntityConfigurationFinder();
            var configItems = configService.FinderAll();
            foreach (var item in configItems)
            {
                var assembly = Assembly.GetAssembly(item);
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }
        }
    }
}
