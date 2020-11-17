using DL.Core.ulitity.ui;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DL.Core.Mediator;
namespace ConsoleApp2
{
    public  class MessageNotify: INotification
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
    public class MessageRequest: IRequest<ReturnResult>
    {
        public string UserName { get; set; }
        public string Message { get; set; }

    }
    public class MessageHanlder :NotifyHandler<MessageNotify>
    {
        public Task Handle(MessageNotify notification, CancellationToken cancellationToken)
        {

            Console.WriteLine($"我在处理Handlem,数据为：{notification.Message}-{notification.UserName}");
            return Task.CompletedTask;
        }
    }
    public class MessageEventHanlder : IRequestExecute<MessageRequest, ReturnResult>
    {
        //async Task<ReturnResult> IRequestHandler<MessageRequest, ReturnResult>.Handle(MessageRequest request, CancellationToken cancellationToken)
        //{
        //    Console.WriteLine($"我在处理数据：{request.UserName}--{request.Message}");
        //    var result = new ReturnResult();
        //    return await Task.FromResult(result);
        //}
        public Task<ReturnResult> Handle(MessageRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"我在处理数据：{request.UserName}--{request.Message}");
               var result = new ReturnResult();
             return  Task.FromResult(result);
        }
    }
}
