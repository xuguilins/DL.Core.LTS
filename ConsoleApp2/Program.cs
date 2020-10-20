using System;
using System.Collections.Generic;
using System.Linq;
using DL.Core.Ado.SqlServer;
using DL.Core.ulitity.ui;
using DL.Core.ulitity.log;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = LogManager.GetLogger();
            logger.Debug("奥里给");

            Console.ReadKey();
        }
    }
   public class UserInfo
    {
        public string UserName { get; set; }
        public string Id { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
