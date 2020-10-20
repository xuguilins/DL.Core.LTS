using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DL.Core.EfCore
{
    public interface IDbContext
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 异步保存数据
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        string ConnectionString { get; set; }

        IUnitOfWork CurrentUnitOfWork { get; set; }

        DatabaseFacade Database { get; }
    }
}
