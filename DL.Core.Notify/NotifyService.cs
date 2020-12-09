using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.Notify
{
    public abstract class NotifyService<TParmars> : INotifyService<TParmars> where TParmars : CommonParams
    {
        public abstract NotifyType NotityType { get;  }
        public abstract object Send(TParmars parmars);
        public abstract void SendVoid(TParmars parmars);

    }
}
