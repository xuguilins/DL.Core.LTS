using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DL.Core.Mediator
{
    public  class MeditaorBus:IMeditaorBus
    {
        public IMediator _mediator;
        public MeditaorBus(IMediator mediator)
        {
            _mediator = mediator;
          
        }
        /// <summary>
        /// 发布通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
       public Task Publish(object notification, CancellationToken cancellationToken = default)=> _mediator.Publish(notification, cancellationToken);

        /// <summary>
        /// 发布通知
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return _mediator.Publish(notification, cancellationToken);
        }
        /// <summary>
        /// 发送命令/命令模式
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) => _mediator.Send(request, cancellationToken);
    }
}
