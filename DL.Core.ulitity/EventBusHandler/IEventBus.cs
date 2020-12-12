using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{
    public interface IEventBus
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event">事件类型</param>
        /// <param name="eventData">事件参数</param>
        void Publish<TEvent>(TEvent @event, object eventData) where TEvent : IEventHandler;
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event">事件参数</param>
        void Publish<TEvent>(TEvent @event) where TEvent : EventData;

        /// <summary>
        /// 移除指定参数的所有事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        void RemoveEvent<TEvent>(TEvent @event) where TEvent : EventData;

        /// <summary>
        /// 移除指定的事件
        /// </summary>
        /// <param name="type">指定的事件</param>
        void RemoveEvent(Type type);
    }
}