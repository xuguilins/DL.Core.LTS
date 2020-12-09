using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.Notify.MailEnttiys
{
    public  class MailEntity:CommonParams
    {
        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 邮件接收人，可以是多个
        /// key:昵称
        /// value:邮箱账号
        /// </summary>
        public Dictionary<string,string> ReciveUser { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string EmailHost { get; set; }
        /// <summary>
        /// 邮件端口
        /// </summary>
        public int Hostportal { get; set; }
        /// <summary>
        /// 邮件f服务器授权密码
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 授权人
        /// </summary>
        public string ServerName { get; set; }
    }
}
