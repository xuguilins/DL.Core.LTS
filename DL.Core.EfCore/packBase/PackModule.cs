using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DL.Core.EfCore.packBase
{
    public abstract class PackModule
    {
        public virtual int OrderLevel { get; set; }
        public abstract IServiceCollection AddService(IServiceCollection services);
              
    }
}
