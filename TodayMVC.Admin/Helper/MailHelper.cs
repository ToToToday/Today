using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TodayMVC.Admin.Helper
{
    public class MailHelper
    {

        public static void SendMail(List<string> recipientList, string subject, string body)
        {
            //服務 郵件 順序無所謂
            string SenderAccount = "jiaxuansecond@gmail.com";
            string Password = "xkmejlozgwvefcxh";



            #region 服務
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587; //通訊埠
            
            //建構式多載寫法
            //SmtpClient client2 = new SmtpClient("smpt.gmail.com", 587);

            //憑證
            client.Credentials = new NetworkCredential(SenderAccount, Password);
            client.EnableSsl = true;
            #endregion

            #region 郵件
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SenderAccount, "Today"); //寄件人

            recipientList.ForEach(addr => mail.To.Add(addr));  //收件人

            mail.Priority = MailPriority.High;
            mail.Subject = subject; //主旨
            mail.SubjectEncoding = Encoding.UTF8;

            //內文
            mail.Body = body;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;

            //附件
            //Attachment attachment = new Attachment(@"c:\falkepath\fake.txt"); //@是【逐字字串常值】，會使得反斜線無效果
            //mail.Attachments.Add(attachment);
            #endregion
            try
            {
                client.Send(mail); //send 是內建方法
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                //attachment.Dispose();
                mail.Dispose();
                client.Dispose();
            }
        }
    }
}
