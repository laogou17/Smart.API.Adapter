using Smart.API.Adapter.Common.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Common
{
    public class SendMailHelper
    {
        public SendMailHelper()
        { 
        }

        /// <summary>
        /// 设置此电子邮件的主题。
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 设置邮件正文。
        /// </summary>
        public string Body { get; set; }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="isAsync">是否异步发送</param>
        public void SendMail(bool isAsync=true)
        {
            MailHelper mail = new MailHelper(isAsync);
            mail.From = CommonSettings.EmailUserName;
            mail.AddReceive(EmailAddrType.To, CommonSettings.EmailTo,"");

            if (string.IsNullOrWhiteSpace(Subject))
            {
                Subject = "这是个标题";
            }
            mail.Subject = Subject;
            if (string.IsNullOrWhiteSpace(Body))
            {
                Body = "这是邮件内容";
            }
            
            // Guid.NewGuid() 防止重复内容，被SMTP服务器拒绝接收邮件
            mail.Body = Body +"_"+ Guid.NewGuid();
            mail.IsBodyHtml = true;

            if (!mail.ExistsSmtpClient())
            {
                SmtpClient client = new SmtpHelper(CommonSettings.EmailSMTP, CommonSettings.EmailPort, CommonSettings.EmailSSL, CommonSettings.EmailUserName, CommonSettings.EmailPassword).SmtpClient;
                mail.AsycUserState = "邮件[" + Subject + "]";
                client.SendCompleted += (send, args) =>
                {
                    AsyncCompletedEventArgs arg = args;

                    if (arg.Error == null)
                    {
                        // 需要注意的事使用 MailHelper 发送异步邮件，其UserState是 MailUserState 类型
                        LogHelper.Info("邮件日志："+((MailUserState)args.UserState).UserState.ToString() + "已发送完成.");
                    }
                    else
                    {
                         LogHelper.Error(String.Format("{0} 异常：{1}"
                            , ((MailUserState)args.UserState).UserState.ToString() + "发送失败."
                            , (arg.Error.InnerException == null ? arg.Error.Message : arg.Error.Message + arg.Error.InnerException.Message)));
                        // 标识异常已处理，否则若有异常，会抛出异常
                        ((MailUserState)args.UserState).IsErrorHandle = true;
                    }
                };
                mail.SetSmtpClient(client, true);
            }
            else
            {
                mail.AsycUserState = "邮件[" + Subject + "]邮件已发送完成。";
            }
            mail.SendOneMail();
            mail.Reset();
        }
    }
}
