﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.EfCore;
using DL.Core.ulitity.ui;

namespace DL.Core.EFCore
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
         private IDbContext _dbContext;
         private DbSet<TEntity> DbSet;
        private IServiceProvider _provider;
        public Repository(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
            var service = _provider.GetService<IUnitOfWork>();
            UnitOfWork = service.GetUnitOfWorkByEntity(typeof(TEntity));
            _dbContext = UnitOfWork.CurrentDbContext;
            DbSet = ((DbContext)_dbContext).Set<TEntity>();
        }

        #region [同步方法]
        public IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        ///跟踪查询实体
        /// </summary>
        public IQueryable<TEntity> TrackEntities => DbSet.AsTracking();

        /// <summary>
        /// 非跟踪查询实体
        /// </summary>
        public IQueryable<TEntity> Entities => DbSet.AsNoTracking().AsQueryable();

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int AddEntity(TEntity entity)
        {
            DbSet.Add(entity);
            // return UnitOfWork.CurrentDbContext.SaveChanges();
            return _dbContext.SaveChanges();

        }

        public int AddEntityItems(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int UpdateEntity(TEntity entity)
        {
            DbSet.Update(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int RemoveEntity(TEntity entity)
        {
            DbSet.Remove(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 根据指定表达式获取实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity GetEntityByExpression(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.FirstOrDefault(expression);
        }

        #endregion [同步方法]

        #region [异步方法]

        /// <summary>
        /// 异步新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public async Task<int> AddEntityAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步新增批量实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> AddEntityItemsAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步更新更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public async Task<int> UpdateEntityAsync(TEntity entity)
        {
            DbSet.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步移除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public async Task<int> RemoveEntityAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步根据指定表达式获取实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<TEntity> GetEntityByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }

        #endregion [异步方法]
    }
}