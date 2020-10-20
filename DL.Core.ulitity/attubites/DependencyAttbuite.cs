using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.attubites
{
    public class DependencyAttbuite:Attribute
    {
        public ServiceLifetime Lifetime { get; }
        public DependencyAttbuite(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
