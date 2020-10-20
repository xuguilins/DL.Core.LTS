using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.EfCore.finderPacks;
using DL.Core.ulitity.finder;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection;
using DL.Core.ulitity.ui;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DL.Core.EfCore
{
  
    public class DbContextBase<TDbContxt>:DbContext, IDbContext where TDbContxt:DbContext
    {
       // private static readonly EntityConcurrentDictory dic = new EntityConcurrentDictory();
      //  private IServiceProvider provider;
        public virtual string ConnectionString { get; set; }
        public IUnitOfWork CurrentUnitOfWork { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
      
        protected  override void OnModelCreating(ModelBuilder modelBuilder)
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
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        public override DatabaseFacade Database => base.Database;

      
    }
}
