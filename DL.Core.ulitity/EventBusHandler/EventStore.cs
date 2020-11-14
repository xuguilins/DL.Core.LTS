using DL.Core.ulitity.log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{
    public class EventStore : IEventStore
    {
        private static ILogger logger = LogManager.GetLogger<EventStore>();
        ConcurrentDictionary<Type, List<Type>> eventDictory = new ConcurrentDictionary<Type, List<Type>>();
       private static  IEventBusFinder finder = new EventBusFinder();
        /// <summary>
        /// 获取所有事件仓促处理器
        /// </summary>
        public ConcurrentDictionary<Type, List<Type>> GetEventHandler()
        {
            var types = finder.FinderAll();
            foreach (var type in types)
            {

                if (typeof(IEventHendler).IsAssignableFrom(type))
                {
                    //获取当前类实现的泛型接口
                    var genter = type.GetInterface("IEventHandler`1");
                    if (genter != null)
                    {
                        //获取泛型接口的参数
                        var parmars = genter.GetGenericArguments();
                        Type obj = null;
                        if (parmars != null && parmars.Length > 0)
                        {
                            obj = parmars[0];
                        }
                        if (eventDictory.ContainsKey(obj))
                        {
                            List<Type> list = eventDictory[obj];
                            list.Add(type);
                            eventDictory[obj] = list;
                        }
                        else
                        {
                            List<Type> list = new List<Type>();
                            list.Add(type);
                            eventDictory.TryAdd(obj, list);
                        }
                    }
                }
            }
            logger.Info($"共获取事件处理器“{eventDictory.Count()}”个", "Event");
            return eventDictory;
        }
    }
}
