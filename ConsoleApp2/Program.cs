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

namespace ConsoleApp2
{
    internal class Program
    {
    

        private static void Main(string[] args)
        {
            var data = new EventData
            {
                EventStartTime = DateTime.Now,
                EventType = EventType.AgreeEvent     
            };

            string a = "12";
            var b = Convert.ChangeType(a, typeof(int));
            Console.ReadKey();
        }   
    }

    public class UserEventHandler : IEventHandler<EventData>
    {
        public void Execute(EventData @event)
        {
            Console.WriteLine($"出发事件：{@event.ToJson()}");
        }
    }
    public class UserRegistCommand : ICommand<ReturnResult>
    {
        private string _userName;
        private string _userMessage;
        public UserRegistCommand(string username,string message) {
            _userMessage = message;
            _userName = username;
        }
        public ReturnResult Execute(object data = null)
        {
            Console.WriteLine($"{_userName}说：{_userMessage}");
            return null;
        }
    }
    public class UserParmasCommand : ICommand<ReturnResult>
    {
        private string _userName;
        private string _userMessage;
        public UserParmasCommand(string username, string message)
        {
            _userMessage = message;
            _userName = username;
        }
        public ReturnResult Execute(object data = null)
        {
            Console.WriteLine($"{_userName}说：{_userMessage},我的参数为：{data.ToJson()}");
            return null;
        }
    }
    public class UserParmasAsycnCommand : ICommand<ReturnResult>
    {
        private string _userName;
        private string _userMessage;
        public UserParmasAsycnCommand(string username, string message)
        {
            _userMessage = message;
            _userName = username;
        }
        public ReturnResult Execute(object data = null)
        {
            Console.WriteLine($"{_userName}说：{_userMessage},我的参数为：{data.ToJson()}");
            return null;
        }
    }

    public class UserRgisetAsyncCommand:ICommand<ReturnResult>
    {
        private string _userName;
        private string _userMessage;
        public UserRgisetAsyncCommand(string username, string message)
        {
            _userMessage = message;
            _userName = username;
        }
        public ReturnResult Execute(object data = null)
        {
            Console.WriteLine($"{_userName}说：{_userMessage},我没有参数");
            return new ReturnResult(ReturnResultCode.Success,null,"奥里给");
        }
    }
   
 
  
  
}

   