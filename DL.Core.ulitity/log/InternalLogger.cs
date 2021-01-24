using DL.Core.ulitity.tools;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace DL.Core.ulitity.log
{
    /// <summary>
    /// 系统内置日志
    /// </summary>
    public class InternalLogger : LogBase
    {
        private static readonly object locker = new object();
        private static readonly ConcurrentDictionary<string, string> pathdic = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">内容</param>
        /// <param name="exception">异常消息</param>
        protected override void Write(LogLevel level, object message,string logexit="Log",Exception exception = null)
        {
            lock (locker)
            {
                DateTime dateTimeNow = DateTime.Now;
                var path = AppDomain.CurrentDomain.BaseDirectory;
                string logDirPath = path+"\\logs";
                if (!logDirPath.CheckDirctoryIsExite())
                {
                    FileExtensition.CreateDic(logDirPath);
                }
                string logFilePath = string.Format("{0}\\{1}.log", logDirPath, $"{logexit}_{level.ToString()}_{ dateTimeNow.ToString("yyyy-MM-dd")}");
                using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.UTF8))
                {
                    try
                    {
                        StackTrace t = new StackTrace();
                        StackFrame f = t.GetFrame(t.FrameCount - 1);
                        object source = f.GetMethod();
                        writer.WriteLine($"[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]  " + source);
                        writer.WriteLine(message + "\r\n");
                        if (exception != null) {
                            writer.WriteLine($"\r\n{exception.Message}\r\n {exception.StackTrace}");
                         } 
                        writer.WriteLine("===============================================================");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    writer.Close();
                }
            }
        }
    }
}