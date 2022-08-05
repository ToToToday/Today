using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Cookie_Auth.Helpers
{
    internal static class MailHelper
    {
        public static void SendMail(List<string> recipientList, string subject, string body)
        {
            string SenderAccount = "chihwen12151215@gmail.com";
            string Password = "rhsyywforuipduvl";

            //服務、郵件，兩大部分，順序無妨
            #region 服務
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587; //port通訊埠：
            // smtp通常是587 ，
            // sqlserver 通常是1433 (通常不會亂改)
            // 80: 通常http
            // 443: https

            //也可以直接利用 建構式多載
            //SmtpClient client = new SmtpClient("smtp.gmail.com" , 587);

            client.Credentials = new NetworkCredential(SenderAccount, Password);
            //憑證
            client.EnableSsl = true;
            #endregion

            #region 郵件
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SenderAccount, "寄信人抬頭");

            recipientList.ForEach(x => mail.To.Add(x));


            mail.Priority = MailPriority.High;
            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.UTF8; //應該是有中文就需要

            mail.Body = body;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;

            //附件
            //Attachment attachment = new Attachment(@"C:\fakepath\fake.txt");
            //mail.Attachments.Add(attachment);
            #endregion

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.Message);
                throw ex;
                //無法寄信的處理
                //https://blog.no2don.com/2021/01/c-gmail-smtp-server-requires-secure.html
                //1. 低安全性應用程式  開啟 (有兩步驟驗證的帳號無法設定)
                //2. 啟用 IMAP

                //==== 2022 / 05 / 30 後 ====
                //不支援低安全性應用程式
                //不支援使用 帳密 就登入

                //處理方式：
                //https://stackoverflow.com/questions/72543208/how-to-use-mailkit-with-google-after-may-30-2022

                //google帳戶管理 > 安全性 >
                // > 兩步驟驗證 > 啟用，並跑一點一些小流程。
                // > 回到原本 兩步驟驗證 的按鈕下，出現【應用程式密碼】功能

                //選郵件，取個名稱 > 會給予 16 字元密碼，需記錄起來。
                //把這個C#寄信方法用的，自己的密碼替換成上方的 16 字元密碼。
            }
            finally
            {
                //倒著拋棄，順序顛倒不知道會不會影響?
                //attachment.Dispose();
                mail.Dispose();
                client.Dispose();
            }
        }
    }
}
