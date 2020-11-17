using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace DL.Core.Mediator
{
   public interface IRequestExecute { }
   public interface IRequestExecute<TCommand,TResult>:IRequestHandler<TCommand, TResult>, IRequestExecute where TCommand : IRequest<TResult>
    {
    }
}
