using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Smart.API.Adapter.Common;

namespace Smart.API.Adapter.Biz
{
    public  class MailManager
    {

        private MailMessage msg = new MailMessage(); 
        public List<string> EmailAddressTo { get; set; }
        public string EmailFrom { get; set; }

        public MailManager()
        {
            EmailAddressTo = new List<string>();
            EmailAddressTo.Add(CommonSettings.EmailTo1);
            EmailFrom = CommonSettings.EmailFrom;
        }

        public void SendMail()    
        {  
            EmailAddressTo.ForEach(x=>msg.To.Add(x));
            /*   
            * msg.To.Add("b@b.com");   
            * msg.To.Add("b@b.com");   
            * msg.To.Add("b@b.com");可以发送给多人   
            */    
            //msg.CC.Add("c@c.com");    
            /*   
            * msg.CC.Add("c@c.com");   
            * msg.CC.Add("c@c.com");可以抄送给多人   
            */
            msg.From = new MailAddress(EmailFrom, "AlphaWu", System.Text.Encoding.UTF8);    
            /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/    
            msg.Subject = "这是测试邮件";//邮件标题    
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码    
            msg.Body = "邮件内容";//邮件内容    
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码    
            msg.IsBodyHtml = false;//是否是HTML邮件    
            msg.Priority = MailPriority.High;//邮件优先级   
  
            SmtpClient client = new SmtpClient();

            //client.Host = "localhost"; 

            //client.Credentials = new System.Net.NetworkCredential("username@zj.com", "userpass");    
            ////在zj.com注册的邮箱和密码    
            //client.Host = "smtp.zj.com";

            //client.Credentials = new System.Net.NetworkCredential("username@gmail.com", "password");
            ////上述写你的GMail邮箱和密码    
            //client.Port = 587;//Gmail使用的端口    
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true;//经过ssl加密     

            object userState = msg;    
            try    
            {    
                client.SendAsync(msg, userState);    
                //简单一点儿可以client.Send(msg);    
                //MessageBox.Show("发送成功");    
            }    
            catch (System.Net.Mail.SmtpException ex)    
            {    
                //MessageBox.Show(ex.Message, "发送邮件出错");    
            }    
        } 
 

    }
}
