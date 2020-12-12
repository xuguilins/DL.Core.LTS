using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Core.Notify.MailEnttiys;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace DL.Core.Notify
{
    public interface IMailSendService : INotifyService<MailEntity>
    {

    }
    public class MailSendService : NotifyService<MailEntity>, IMailSendService
    {
        public override NotifyType NotityType => NotifyType.Email;

        public override object Send(MailEntity parmars)
        {
            try
            {
                var message = new MimeMessage();
                List<MailboxAddress> list = new List<MailboxAddress>();
                if (parmars.ReciveUser.Count > 0)
                {
                    foreach (var item in parmars.ReciveUser)
                    {
                        var value = parmars.ReciveUser[item.Key];
                        list.Add(new MailboxAddress(Encoding.UTF8, item.Key, value));
                    }
                }
                message.From.Add(new MailboxAddress(parmars.FromUserName, parmars.FromUser));
                if (!list.Any())
                    return "请检查是否含有收件人";
                message.To.AddRange(list);
                message.Subject = parmars.Subject;
                message.Body = new TextPart("plain")
                {
                    Text = parmars.Content
                };
                using (var client = new SmtpClient())
                {

                    client.Connect(parmars.EmailHost, parmars.Hostportal, false);
                    client.Authenticate(parmars.ServerName, parmars.Secret);
                    client.Send(message);
                    client.Disconnect(true);
                };
                return new
                {
                    Code = 0,
                    Message = "邮件发送成功"
                };
            }
            catch (Exception ex)
            {
                return $"邮件发送失败，失败原因:[{ex.Message}]";
            }
         
        }

        public override void SendVoid(MailEntity parmars)
        {
          
        }
    }
}
