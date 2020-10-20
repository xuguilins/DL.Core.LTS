using DL.Core.EfCore.finderPacks;
using DL.Core.EfCore.packBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.EfCore.engine
{
    public static class EnginePack
    {
        public static IServiceCollection  AddEnginePack<TDContext>(this IServiceCollection  services) where TDContext:DbContext
        {
            services.AddDbContext<TDContext>();
            var packFinder = new PackModuleFinder();
            var packTypes= packFinder.FinderAll();
            foreach (var item in packTypes)
            {
                var pack = Activator.CreateInstance(item) as PackModule;
                pack.AddService(services);
            }
            var provider = services.BuildServiceProvider();
            ServiceLocator.Instace.SetProvider(provider);
            return services;
        } 
     
    }
}
