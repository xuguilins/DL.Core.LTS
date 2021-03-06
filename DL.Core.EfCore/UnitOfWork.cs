using DL.Core.EfCore.finderPacks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {     
        public IServiceProvider provider;
        private bool _beginTransaction = false;
        private IDbContextTransaction _dbContextTransaction;
        public IDbContext CurrentDbContext { get; private set; }
        public UnitOfWork(IServiceProvider serviceProvider)
        {
            provider = serviceProvider;
        }
        public IUnitOfWork CurrentUnitOfWork { get; private set; }

        public bool BeginTransaction
        {
            get
            {
                return _beginTransaction;
            } set
            {
                if (value)
                {
                    _dbContextTransaction=CurrentDbContext.Database.BeginTransaction();
                    _beginTransaction = value;
                }
            }
        } 

        public IUnitOfWork GetUnitOfWorkByEntity(Type type)
        {       
           var context = GetDbContextByEntity(type);
           CurrentDbContext = context;
           context.CurrentUnitOfWork = this;
           CurrentUnitOfWork = this;
           return this;
        }
        public IDbContext GetDbContextByEntity(Type type)
        {

            //获取所有实体配置
            var service = provider.GetService<IEntityConfigurationFinder>();
            var items = service.FinderAll().Select(x => Activator.CreateInstance(x) as IEntityTypeRegiest)
                .ToList();
            var dbType = items.FirstOrDefault(x => x.EntityType == type)?.DbContextType;
            var context = Activator.CreateInstance(dbType) as IDbContext;
            var scopeDictory = provider.GetService<ScopedDictory>();
            scopeDictory.TryAdd($"dbcontext-{context.ConnectionString}", context);
            scopeDictory.TryAdd($"dbentity-{type.Name}", context);
            return context;
        }

        public void CommitTransaction()
        {
            try
            {
                _dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally
            {
                if (_dbContextTransaction!=null)
                {
                    _dbContextTransaction.Dispose();
                    _dbContextTransaction = null;
                    CurrentDbContext.CurrentUnitOfWork = null;
                    CurrentUnitOfWork = null;
                    
                }

            }
        }
    }
}
