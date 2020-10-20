using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.EfCore
{
    public  class ServiceLocator
    {
        public IServiceProvider ServiceProvider { get;  set; }
        public static readonly Lazy<ServiceLocator> locator = new Lazy<ServiceLocator>(() => new ServiceLocator());
        public static ServiceLocator Instace => locator.Value;
        public void SetProvider(IServiceProvider provider)
        {
            ServiceProvider = provider;
        }

         public T GetService<T>()
        {
            if (ServiceProvider == null)
                return default(T);
            return ServiceProvider.GetService<T>();
            
              
        }
        public object GetService(Type type)
        {
            if (ServiceProvider == null)
                return new object();
            return ServiceProvider.GetService(type);
        }
        public IEnumerable<T> GetServices<T>()
        {
            if (ServiceProvider == null)
                return default;
            return ServiceProvider.GetServices<T>();
        }

        public IEnumerable<object> GetServices(Type type)
        {
            if (ServiceProvider == null)
                return null;
            return  ServiceProvider.GetServices(type);
        }
        public T GetRequiredService<T>()
        {
            if (ServiceProvider == null)
                return default(T);
            return ServiceProvider.GetRequiredService<T>();


        }
        public object GetRequiredService(Type type)
        {
            if (ServiceProvider == null)
                return new object();
            return ServiceProvider.GetRequiredService(type);
        }
    }
}
