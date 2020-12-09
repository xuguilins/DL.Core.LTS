using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.Notify
{
    /// <summary>
    /// 公共参数
    /// </summary>
    public  class CommonParams
    {
        /// <summary>
        /// 发送人
        /// </summary>
        public  string FromUser { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public virtual string[] ToUser { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }
    }
}
