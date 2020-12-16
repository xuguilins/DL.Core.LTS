using System;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ulitity.log;
using DL.Core.ulitity.EventBusHandler;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using DL.Core.Mediator;
using System.IO;
using System.Text;
using System.Collections.Generic;
using DL.Core.ulitity.tools;
using System.Linq;
using DL.Core.EfCore.engine;
using DL.Core.EfCore.packBase;
using DL.Core.ulitity.attubites;
using System.ComponentModel;
using System.Threading.Tasks;
using DL.Core.ulitity.ui;
using DL.Core.ulitity.CommandBuilder;
using System.Data;
using Quartz.Core;
using Quartz;
using Quartz.Util;
using Quartz.Impl;
namespace ConsoleApp2
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            //调度器
            IScheduler scheduler;
            //调度器工厂
            ISchedulerFactory factory;
            //创建一个调度器
            Console.WriteLine($"当前时间=--{DateTime.Now}");
            factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler().Result;
            IJobDetail job = JobBuilder.Create<UserNotifyTask>().WithIdentity("job1", "group1").Build();
            //job.JobDataMap.Add()
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("trigger1", "group1")
              .StartAt(DateTime.Now.AddSeconds(5))
              .Build();
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
            Console.ReadKey();
        }
    }
    public class UserNotifyTask : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("我是用户提醒"+DateTime.Now);
            return Task.CompletedTask;
           
        }
    }
}

   