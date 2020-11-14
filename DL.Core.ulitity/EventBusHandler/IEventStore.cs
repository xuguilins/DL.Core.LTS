using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{
    public interface IEventStore
    {
        /// <summary>
        /// 获取所有事件仓促处理器
        /// </summary>
        ConcurrentDictionary<Type, List<Type>> GetEventHandler();
    }
}
