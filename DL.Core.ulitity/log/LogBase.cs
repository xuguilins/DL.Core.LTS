using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.log
{
    /// <summary>
    /// 日志基类
    /// </summary>
    public abstract class LogBase : ILogger
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常消息</param>
        protected abstract void Write(LogLevel level, object message, string logexit = "Log", Exception exception = null);

        /// <summary>
        /// 写入调试级别的日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception">异常信息</param>
        public virtual void Debug(string message, string logexit = "Log", Exception exception = null)
        {
            Write(LogLevel.Debug, message,logexit, exception);
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception">异常信息</param>
        public virtual void Error(string message, string logexit = "Log", Exception exception = null)
        {
            Write(LogLevel.Error, message,logexit, exception);
        }

        /// <summary>
        /// 写入正常日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception">异常信息</param>
        public virtual void Info(string message, string logexit = "Log", Exception exception = null)
        {
            Write(LogLevel.Info, message,logexit, exception);
        }

        /// <summary>
        /// 写入成功日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception">异常信息</param>
        public virtual void Success(string message, string logexit = "Log", Exception exception = null)
        {
            Write(LogLevel.Success, message,logexit, exception);
        }

        /// <summary>
        /// 写入警告日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception">异常信息</param>
        public virtual void Warn(string message, string logexit = "Log", Exception exception = null)
        {
            Write(LogLevel.Warn, message,logexit,exception);
        }
    }
}