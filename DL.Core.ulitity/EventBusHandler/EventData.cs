using DL.Core.ulitity.tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{
    public enum EventType
    {
        /// <summary>
        /// 成功事件
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败事件
        /// </summary>
        Error = 1,

        /// <summary>
        /// 警告事件
        /// </summary>
        Warn = 2,

        /// <summary>
        /// 正常事件
        /// </summary>
        Info = 3,
        /// <summary>
        /// 创建事件
        /// </summary>
        Create = 4,
        /// <summary>
        /// 更新事件
        /// </summary>
        Update = 5,
        /// <summary>
        /// 删除事件
        /// </summary>
        Delete = 6,
        /// <summary>
        /// 引擎启动事件
        /// </summary>

        EngineStart = 7,
        /// <summary>
        /// 引擎结束事件
        /// </summary>
        EngineEnd = 8,
        /// <summary>
        /// 引擎停止事件
        /// </summary>
        EngineStop = 9,
        /// <summary>
        /// 过期事件
        /// </summary>
        ExpireEvent = 10
    }
    public class EventData
    {
        public EventType EventType { get; set; }
        public DateTime EventStartTime { get; set; }
        public string EventId => "Event" + StrHelper.GetDateGuid();
    }
}
