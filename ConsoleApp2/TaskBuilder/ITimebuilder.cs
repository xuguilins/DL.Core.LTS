using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.TaskBuilder
{
    /// <summary>
    /// 定时任务接口
    /// </summary>
    public interface ITimebuilder
    {
        /// <summary>
        /// 每天执行任务，默认晚上12点
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        void DateTimeOfEverDay<TJob>() where TJob : IJob;
        /// <summary>
        /// 每天指定时间执行任务
        /// 一但指定，即每天这个时候都会执行
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="time">指定时间/每天执行</param>
        void DateTimeOfEverDay<TJob>(DateTime time) where TJob : IJob;
        /// <summary>
        /// 每天执行任务，默认晚上12点
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="dic">参数</param>
        void DateTimeOfEverDay<TJob>(Dictionary<string,object> dic) where TJob : IJob;
        /// <summary>
        /// 每天指定时间执行任务
        /// 一但指定，即每天这个时候都会执行
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="time">指定时间/每天执行</param>
        /// <param name="dic">参数</param>
        void DateTimeOfEverDay<TJob>(DateTime time,Dictionary<string, object> dic) where TJob : IJob;
        /// <summary>
        /// 延迟执行,指定当前时间的后几天执行
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="day">天数</param>
        void DateTimeOfAftearDay<TJob>(int day) where TJob : IJob;

        /// <summary>
        /// 延迟执行,指定当前时间的后几天执行
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="day">天数</param>
        /// <param name="dic">参数</param>
        void DateTimeOfAftearDay<TJob>(int day,Dictionary<string,object> dic) where TJob : IJob;
        /// <summary>
        /// 指定CORN表达式执行
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="cornExpress">表达式</param>
        void ExecuteTask<TJob>(string cornExpress) where TJob : IJob;

        /// <summary>
        /// 指定CORN表达式执行
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="cornExpress">表达式</param>
        /// <param name="dic">参数</param>
        void ExecuteTask<TJob>(string cornExpress,Dictionary<string, object> dic) where TJob : IJob;
    }
}
