using System;
using System.Collections.Generic;
using System.Linq;
using DL.Core.Ado.SqlServer;
using DL.Core.ulitity.ui;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ulitity.finder;
using DL.Core.EfCore.finderPacks;
using DL.Core.EfCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DL.Core.ns.EFCore;
using DL.Core.EfCore.engine;
using System.Runtime.CompilerServices;
using DL.Core.ulitity.attubites;
using DL.Core.ulitity.configer;
using DL.Core.ulitity.log;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using DL.Core.ulitity.tools;
using System.Reflection;
using System.Collections.Concurrent;
using DL.Core.ulitity.EventBusHandler;

namespace ConsoleApp2
{
    class Program
    {
        static ILogger logger = LogManager.GetLogger();

        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IEventBus, EventBus>();
            var provider = services.BuildServiceProvider();
            var service = provider.GetService<IEventBus>();
          
            service.Puslish(new UserEventData { UserName = "张三", EventStartTime = DateTime.Now, EventType = EventType.Create, Message = "我是张三,我创建了事件" });

            Console.ReadKey();
        }
    }
    public class UserEventData : EventData
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
    public class UserEventService : IEventHandler<UserEventData>
    {
        public void Execute(UserEventData @event)
        {
            Console.WriteLine($"执行事件--{@event.UserName}--{@event.Message},EventData:{@event.EventId}-{@event.EventStartTime}-{@event.EventType},Message:{@event.Message}");
        }
    }
    public class TESTService: IEventHandler<UserEventData>
    {
        public void Execute(UserEventData @event)
    {
        Console.WriteLine($"执行事件TESTService--{@event.UserName}--{@event.Message},EventData:{@event.EventId}-{@event.EventStartTime}-{@event.EventType},Message:{@event.Message}");
    }
}




}
 