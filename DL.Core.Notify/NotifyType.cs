using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DL.Core.Notify
{
    public enum NotifyType
    {
       [Description("微信提醒")]
        WX = 0,
       [Description("邮件提醒")]
        Email = 1,
       [Description("钉钉提醒")]
        DingTalk = 2,
       [Description("短信提醒")]
        SMS = 3
    }
}
