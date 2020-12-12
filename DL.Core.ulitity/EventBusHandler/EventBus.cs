using DL.Core.ulitity.log;
using DL.Core.ulitity.tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{
    public class EventBus : IEventBus
    {
        private IEventStore service = new EventStore();
        private ConcurrentDictionary<Type, List<Type>> HandlerData = null;
        private static ILogger logger = LogManager.GetLogger<EventBus>();

        public EventBus()
        {
            HandlerData = service.GetEventHandler();
        }

        public void Publish<TEvent>(TEvent @event,object eventData) where TEvent : IEventHandler
        {
            var type = eventData.GetType();
            if (type != null)
            {
                
                //获取当前事件继承的接口以及参数
                var list = HandlerData[type];
                foreach (var item in list)
                {
                    var method = item.GetMethod("Execute");
                    if (method != null)
                    {
                        logger.Info($"执行事件：{item.Name},参数:{eventData.ToJson()}", "Event");
                        var instance = Activator.CreateInstance(item);
                        method.Invoke(instance, new object[] { eventData });
                        logger.Info($"成功执行了一个事件---{item.Name}", "Event");
                    }
                }
            }
        }

        /// <summary>
        /// 发布指定的事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        public void Publish<TEvent>(TEvent @event) where TEvent : EventData
        {
            //获取当前事件继承的接口
            var type = @event.GetType();//IEventHandler`1
            if (HandlerData.ContainsKey(type))
            {
                var handlers = HandlerData[type];
                if (handlers != null && handlers.Count > 0)
                {

                    foreach (var item in handlers)
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

        /// <summary>
        /// 移除指定的参数的所有事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        public void RemoveEvent<TEvent>(TEvent @event) where TEvent : EventData
        {
            var type = @event.GetType();
            List<Type> types = new List<Type>();
            HandlerData.TryRemove(type, out types);
        }

        /// <summary>
        /// 移除指定的事件
        /// </summary>
        /// <param name="type"></param>
        public void RemoveEvent(Type type)
        {
            foreach (var key in HandlerData.Keys)
            {
                var list = HandlerData[key];
                var model = list.FirstOrDefault(x => x == type);
                if (model != null)
                {
                    list.Remove(model);
                    logger.Info($"成功移除了一个事件：{model.Name}", "Event");
                    HandlerData[key] = list;
                }
            }
        }
    }
}