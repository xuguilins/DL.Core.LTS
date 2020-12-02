using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace DL.Core.Mediator
{
   public static  class MeditaorExtendsiton
    {
        /// <summary>
        /// 添加中介者Pack
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection UseMeditorPack(this IServiceCollection services)
        {
            IMeditaorFinder finder = new MeditaorFinder();
            var types = finder.FinderAll().ToArray();
            services.AddMediatR(types);
            services.AddScoped<IMeditaorBus, MeditaorBus>();
            return services;
        }
    }
}
