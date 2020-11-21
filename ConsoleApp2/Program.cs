using System;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ulitity.log;
using DL.Core.ulitity.EventBusHandler;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using DL.Core.Mediator;
using DL.Core.ulitity.tools;
using System.Collections.Generic;

namespace ConsoleApp2
{
    internal class Program
    {
        private static ILogger logger = LogManager.GetLogger();
        private static Dictionary<string, string> dic = new Dictionary<string, string>
        {
            {"千","000" },
            {"百","00" },
            {"万","0000" },
            {"十","0" }
        };
        private static Dictionary<char, int> mdic = new Dictionary<char, int>
        {
            {'一',1},
            {'二',2 },
            {'三',3 },
            {'四',4 },
            {'五',5 },
            {'六',6 },
            {'七',7 },
            {'八',8 },
            {'九',9 },
        };
        private static Dictionary<char, string> xdic = new Dictionary<char, string>
        {
            {'万',"0000" },
            {'元',"" },
        };
        private static void Main(string[] args)
        {
            var str = "一千二百五十万";
            string[] memory = new string[2];
            bool isClear = false;
            List<string> list = new List<string>(); 
            foreach (char item in str)
            {
                if (isClear)
                {
                    var res = memory[0] + memory[1];
                    list.Add(res);
                    Array.Clear(memory, 0, 2);
                    isClear = false;
                }
                var itemkey = item.ToString();
               if (dic.ContainsKey(itemkey))
               {
                    memory[1] = dic[itemkey];
                    isClear = true;
               } else if (mdic.ContainsKey(item))
                {
                    memory[0] = mdic[item].ToString();
                } else
                {

                }
            }
             
            Console.ReadKey();
        }

       
    }

    public class UserEventData : EventData
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }

    public class UserEventService : IEventHandler<UserEventData>
    {
        public void Execute(UserEventData @event)
        {
            Console.WriteLine($"执行事件--{@event.UserName}--{@event.Message},EventData:{@event.EventId}-{@event.EventStartTime}-{@event.EventType},Message:{@event.Message}");
        }
    }

    public class TESTService : IEventHandler<UserEventData>
    {
        public void Execute(UserEventData @event)
        {
            Console.WriteLine($"执行事件TESTService--{@event.UserName}--{@event.Message},EventData:{@event.EventId}-{@event.EventStartTime}-{@event.EventType},Message:{@event.Message}");
        }
    }
}