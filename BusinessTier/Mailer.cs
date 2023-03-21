using System;
using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;
using System.Configuration;

namespace BusinessTier
{

    public class Emailer
    {

        public static async Task<string> SendGridMailMethod(string to, string bcc, string cc, string subject, string body)
        {
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_APIKEY"];
            var client = new SendGridClient(apiKey);

            var toEmail = new EmailAddress(to);
            var fromEmail = new EmailAddress(ConfigurationManager.AppSettings["LOTTOTRY_FROM_IMAIL"]);

            var msg = MailHelper.CreateSingleEmail(
                                                    fromEmail,
                                                    toEmail,
                                                    subject,
                                                    null,
                                                    body
                                                  );
            try
            {
                var response = await client.SendEmailAsync(msg);
                if (response.IsSuccessStatusCode)
                    return null;
                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public const string EmailSender = "info@lottotry.com";
        public static void LottoTryMailMethod(string to, string bcc,
                                  string cc, string subject, string body)
        {
            // access webconfig here
            System.Configuration.Configuration myConfig
                = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(
                  HttpContext.Current.Request.ApplicationPath);

            // get mail settings node in web.config
            System.Net.Configuration.MailSettingsSectionGroup mailSettings
                = ((System.Net.Configuration.MailSettingsSectionGroup)myConfig.GetSectionGroup(
                    "system.net/mailSettings"));

            // get credentials from web.config file under mailsettings node
            System.Net.NetworkCredential myCredential
                = new System.Net.NetworkCredential(
                   mailSettings.Smtp.Network.UserName, mailSettings.Smtp.Network.Password);

            // get from email address
            string from = mailSettings.Smtp.From;

            //create an SMTP client - this can be set in SMTP server under IIS
            System.Net.Mail.SmtpClient myClient
                = new System.Net.Mail.SmtpClient();
            myClient.Host = mailSettings.Smtp.Network.Host;
            myClient.UseDefaultCredentials = false;
            myClient.Credentials = myCredential;

            // email message:
            MailMessage mailMessage = new MailMessage();
            mailMessage.From
                        = new System.Net.Mail.MailAddress(from); // from

            if (to != null)                                      // to
            {
                mailMessage.To.Add(to);
            }



#if true
            mailMessage.Bcc.Add(mailMessage.From);              // bcc

#else
            mailMessage.Bcc.Add(from);
            mailMessage.Bcc.Add("henryma14@gmail.com");
            mailMessage.Bcc.Add("hma14@shaw.ca");

#endif
            mailMessage.Subject = subject;                   // subject
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;                         // body
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            try
            {
                myClient.Send(mailMessage);                // send it
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // this method goes in the business layer
        public static string SendEmailFromGoDaddy(string subject, string body, string sender,
                string recipient, string bcc, bool isHTML, string smtpUsername, string smtpPassword)
        {
            string msg = "";

            try
            {
                MailMessage mailMsg = new MailMessage();
                if (recipient != "")
                {
                    mailMsg.To.Add(recipient);
                }
                if (bcc != "")
                {
                    mailMsg.Bcc.Add(bcc);
                }
                mailMsg.Subject = subject;
                mailMsg.Body = body;

                mailMsg.IsBodyHtml = isHTML;

                SmtpClient smtp = new SmtpClient("m1pismtp01-v01.prod.mesa1.secureserver.net", 25);
                //SmtpClient smtp = new SmtpClient("relay-hosting.secureserver.net", 25);
                //smtp.Host = "relay-hosting.secureserver.net";
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(mailMsg);
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
            return msg;   // If msg == null then the e-mail was sent without errors
        }
    }
}


