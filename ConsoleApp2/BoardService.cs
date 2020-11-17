
using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.Mediator;
namespace ConsoleApp2
{
    public class BoardService: IBoardService
    {
        
        private IMeditaorBus _mediator;
        public BoardService(IMeditaorBus mediator)
        {
            _mediator = mediator;
        }
        public void Speak()
        {
           // _mediator.Publish(new MessageNotify { UserName = "语文" , Message="你是猪吗"});

           _mediator.Send(new MessageRequest { UserName="奥里给", Message="是立刻解放拉萨解放拉萨解放拉萨发" });
        }
    }
     public interface IBoardService
    {
        void Speak();
    }
}
