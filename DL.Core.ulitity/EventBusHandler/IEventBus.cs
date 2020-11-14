using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{
    public interface IEventBus
    {
        /// <summary>
        /// 事件发布
        /// </summary>
        /// <typeparam name="TEvent">事件</typeparam>
        /// <param name="event">事件参数</param>
        void Puslish<TEvent>(TEvent @event) where TEvent : EventData;
    }
}
