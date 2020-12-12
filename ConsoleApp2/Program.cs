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

namespace ConsoleApp2
{
    internal class Program
    {
    

        private static void Main(string[] args)
        {
           // .ex
          // /./ ICommandExecutetor service = new CommanndExecutetor();
           // service.Execute(new UserRegistCommand("张三","我是同步的无参命令执行者"));
            object name = new
            {
                userName = "参数A",
                passsWord="使得房价来说手动阀沙"
            };
            CommandRunner.Instance.CommandExecutetor.Execute(new UserParmasCommand("李四", "我是同步的有参数命令执行者"), name);
            Console.WriteLine("下面的是异步方法");
            //service.ExecuteAsync(new UserRgisetAsyncCommand("老外", "我是异步的无参数命令执行者"));
            Console.WriteLine("异步命令A执行完毕");
            //service.ExecuteAsync(new UserParmasAsycnCommand("昂佩里格", "我是异步有参数的命令执行者"), name);
            Console.WriteLine("异步命令B执行完毕");

            Console.ReadKey();
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

   