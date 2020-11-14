using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{
    public interface IEventHendler { }
    public interface IEventHandler<TEvent> : IEventHendler where TEvent : EventData
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="event">事件</param>
        void Execute(TEvent @event);
    }
}
