using DL.Core.EfCore.finderPacks;
using DL.Core.ulitity.attubites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DL.Core.EfCore.packBase
{
    /// <summary>
    /// 注入包
    /// </summary>
    public class DependencyPack : PackModule
    {
        public override int OrderLevel => 10;
        public override IServiceCollection AddService(IServiceCollection services)
        {
            var finder = new DependencyFinder();
            var types= finder.FinderAll();
            foreach (Type type in types)
            {
                var interfance = type.GetInterfaces().FirstOrDefault(x => x.IsInterface && !x.IsDefined(typeof(IgnoreDependencyAttbuite), false));
                if (interfance != null)
                {
                    if (typeof(IScopeDependcy).IsAssignableFrom(interfance))
                    {
                        services.AddScoped(interfance, type);
                    }
                    else if (typeof(ITransientDependcy).IsAssignableFrom(interfance))
                    {
                        services.AddTransient(interfance, type);
                    }
                    else if (typeof(ISingletonDependcy).IsAssignableFrom(interfance))
                    {
                        services.AddSingleton(interfance, type);
                    }
                    else
                    {
                        services = AddAttbuiteDependenty(services, interfance, type);
                    }
                }
            }
            return services;    
        }
        private IServiceCollection AddAttbuiteDependenty(IServiceCollection services, Type interfance, Type type)
        {
            //检查当前类的特性
            var attbuite = type.GetCustomAttributes(false);
            if (attbuite != null && attbuite.Length > 0)
            {
                var attb = attbuite[0] as DependencyAttbuite;
                if (attb != null)
                {
                    switch (attb.Lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(interfance, type);
                            break;

                        case ServiceLifetime.Singleton:
                            services.AddSingleton(interfance, type);
                            break;

                        case ServiceLifetime.Transient:
                            services.AddTransient(interfance, type);
                            break;
                    }
                }
            }
            return services;
        }
    }
}
