using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DL.Core.Mediator
{
    public interface NotifyHandler { }
    public interface NotifyHandler<TNotify>:INotificationHandler<TNotify>, NotifyHandler where TNotify: INotification
    {
    }
}
