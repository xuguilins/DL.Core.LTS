using System;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ulitity.log;
using DL.Core.ulitity.EventBusHandler;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using DL.Core.Mediator;

namespace ConsoleApp2
{
    internal class Program
    {
        private static ILogger logger = LogManager.GetLogger();

        private static void Main(string[] args)
        {
         
           IServiceCollection services = new ServiceCollection();
            services.AddMeditorPack();
            services.AddScoped<IBoardService, BoardService>();
           var provider = services.BuildServiceProvider();
           var service = provider.GetService<IBoardService>();
            service.Speak();

             //services.AddMediatR()

            Console.ReadKey();
        }

        private static UserEventData Test()
        {
            return new UserEventData();
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

    public class TESTService : IEventHandler<UserEventData>
    {
        public void Execute(UserEventData @event)
        {
            Console.WriteLine($"执行事件TESTService--{@event.UserName}--{@event.Message},EventData:{@event.EventId}-{@event.EventStartTime}-{@event.EventType},Message:{@event.Message}");
        }
    }
}