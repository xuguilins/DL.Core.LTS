
using DL.Core.EFCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.EfCore.packBase
{
    public class EFDependencyPack:PackModule
    {
        public override int OrderLevel => 20;
        public override IServiceCollection AddService(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ScopedDictory));
            return services;
        }
    }
}
