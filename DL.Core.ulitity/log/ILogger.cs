﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.log
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 消息日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        void Info(string message, string logexit = "Log", Exception exception = null);

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        void Error(string message, string logexit = "Log", Exception exception = null);

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        void Warn(string message, string logexit = "Log", Exception exception = null);

        /// <summary>
        /// 成功日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        void Success(string message, string logexit = "Log", Exception exception = null);

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        void Debug(string message, string logexit = "Log", Exception exception = null);
    }
}