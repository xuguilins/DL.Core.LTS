using DL.Core.EfCore.finderPacks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.EfCore.packBase
{
    public class FinderPack : PackModule
    {
        public override int OrderLevel => 30;
        public override IServiceCollection AddService(IServiceCollection services)
        {
            services.AddScoped<IDependencyFinder, DependencyFinder>();
            services.AddScoped<IEntityBaseFinder, EntityBaseFinder>();
            services.AddScoped<IEntityConfigurationFinder, EntityConfigurationFinder>();
            services.AddScoped<IPackModuleFinder, PackModuleFinder>();
            return services;
        }
    }
}
