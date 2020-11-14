using DL.Core.ulitity.log;
using DL.Core.ulitity.tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{

    public class EventBus : IEventBus
    {
        private IEventStore service = new EventStore();
        private  ConcurrentDictionary<Type, List<Type>> HandlerData = null;
        private static ILogger logger = LogManager.GetLogger<EventBus>();
        public EventBus()
        {
            HandlerData = service.GetEventHandler();
        }
        public void Puslish<TEvent>(TEvent @event) where TEvent : EventData
        {
            var type = @event.GetType();
            if (type != null)
            {
                var list = HandlerData[type];
                foreach (var item in list)
                {
                    var method = item.GetMethod("Execute");
                    if (method != null)
                    {
                        logger.Info($"执行事件：{item.Name},参数:{@event.ToJson()}", "Event");
                        var instance = Activator.CreateInstance(item);
                        method.Invoke(instance, new object[] { @event });
                        logger.Info($"成功执行了一个事件---{item.Name}", "Event");

                    }
                }
            }
        }
    }
}
