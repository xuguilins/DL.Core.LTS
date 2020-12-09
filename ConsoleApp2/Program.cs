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

namespace ConsoleApp2
{
    internal class Program
    {
        private static ILogger logger = LogManager.GetLogger();
         static Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"个","-1" },
                {"十","0" },
                {"百","00" },
                {"千","000" },
                {"万","0000" },
                {"十万","00000" },
                {"百万","000000" },
                {"千万","0000000" },
                {"亿","00000000" }
            };
        static Dictionary<string, int> pars = new Dictionary<string, int>
        {
            {"一",1 },
            {"二",2 },
            {"三",3 },
            {"四",4 },
            {"五",5 },
            {"六",6 },
            {"七",7 },
            {"八",8 },
            {"九",9 }

        };

        private static void Main(string[] args)
        {

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(StrHelper.GetDateGuid());
            }
            Console.ReadKey();
        }
        private static void SetValue(string func,Func<string,string> callback)
        {
            callback(func);
        }
        private static void ToNumberMoney(string chineseMoney)
        {
            List<string> list = new List<string>();
            string res = string.Empty;
            string wres = string.Empty;
            foreach (char item in chineseMoney)
            {
                string numberStr = item.ToString();
                if(pars.ContainsKey(numberStr))
                {
                    res += pars[numberStr].ToString();
                } else if (dic.ContainsKey(numberStr))
                {
                    if(numberStr == "万")
                    {
                        wres = dic[numberStr];
                    } else
                    {
                        res += dic[numberStr];
                        list.Add(res);
                        res = string.Empty;
                    }  
                } else
                {
                    list.Add(res);
                }
               
            }
            int sum=list.Select(x => x.ToInt32()).Sum();
            Console.WriteLine(sum);
        }

    }
    public interface IUserSerice
    {

    }
    [DependencyAttbuite(ServiceLifetime.Scoped)]
    public class UserService : IUserSerice
    {

    }
    public enum UserType
    {
        [Description()]
        nane = 1
    }
}

   