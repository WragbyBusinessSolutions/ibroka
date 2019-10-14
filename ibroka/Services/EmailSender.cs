using ibroka.Helpers;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ibroka.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        SmtpClient SmtpServer;
        string MailerResponse;


    string apiKey = "";
        public EmailSender()
        {

            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            SmtpServer = smtpClient;
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailerResponse = "";




        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        //public async Task<string> SendGridEmailAsync(string emailAdd, string subject, string message)
        //{
        //}

        public string SendPlainEmailAsync(string emailAdd, string subject, string message)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("spoc@wragbysolutions.com", "iiiii");

            //SmtpServer.Credentials = new System.Net.NetworkCredential("Wragbydev@wragbysolutions.com", "@Devops19");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                //mail.From = new MailAddress("Wragbydev@wragbysolutions.com");
                mail.From = new MailAddress("spoc@wragbysolutions.com");
                mail.To.Add(emailAdd);
                mail.Subject = subject;
                mail.Body = message;
                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }

        public string SendLinkEmailAsync(string emailAdd, string subject, string message)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("spoc@wragbysolutions.com", "iiii");

            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                //mail.From = new MailAddress("Wragbydev@wragbysolutions.com");
                mail.From = new MailAddress("spoc@wragbysolutions.com");
                mail.To.Add(emailAdd);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }


        async Task IEmailSender.SendGridEmailAsync(string emailAdd, string subject, string message, string organisation, string firstname, string template)
        {
            //var apiKey = "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";
            //new code

            string domain = "https://ncribstaging.azurewebsites.net";

            EmailTemplateHelper EmailHelper = new EmailTemplateHelper();

            var body = EmailHelper.GetTemplate(template).Replace("#FirstName", firstname).Replace("#ResetLink", message).Replace("#HostDomain", domain).Replace("#Organisation", organisation);

            //string 

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("no-reply@ibrokang.com", "iBROKA"),
                Subject = "Set Up Your Account on iBROKA.",
                PlainTextContent = "You have been registered on iBROKA platform. Kindly use the link below to create your account.",
                HtmlContent = body


            };
            msg.AddTo(new EmailAddress(emailAdd, emailAdd));
            await client.SendEmailAsync(msg);
            //var response = 

        }

        async Task IEmailSender.SendGridEmailConfrimationAsync(string emailAdd, string subject, string message, string firstname)
        {
            //var apiKey = "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";
            //new code

            string domain = "https://ncribstaging.azurewebsites.net";

            EmailTemplateHelper EmailHelper = new EmailTemplateHelper();

            var body = EmailHelper.GetTemplate("accoutConfirmation").Replace("#FirstName", firstname).Replace("#ResetLink", message).Replace("#HostDomain", domain);

            //string 

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("no-reply@ibrokang.com", "iBROKA"),
                Subject = "Account Confirmationon iBROKA.",
                PlainTextContent = "You have are registered on iBROKA platform. Kindly confrim your account.",
                HtmlContent = body


            };
            msg.AddTo(new EmailAddress(emailAdd, emailAdd));
            await client.SendEmailAsync(msg);
            //var response =

        }
        async Task IEmailSender.SendGridEmailWithAttachmentAsync(string emailAdd, string[] emailCC, string subject, string message, string base64string, string fileName)
        {
            //var apiKey = "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";

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("no-reply@ibrokang.com", "iBROKA"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message

            };
            msg.AddAttachment(fileName, base64string);
            msg.AddTo(new EmailAddress(emailAdd, emailAdd));

            if (!string.IsNullOrEmpty(emailCC[0]) && !string.IsNullOrEmpty(emailCC[1]))
            {
                List<EmailAddress> emails = new List<EmailAddress>();
                if (emailCC[0] != emailAdd && emailCC[0] != emailCC[1])
                {
                    emails.Add(new EmailAddress(emailCC[0], emailCC[0]));
                }
                if (emailCC[1] != emailAdd && emailCC[0] != emailCC[1])
                {
                    emails.Add(new EmailAddress(emailCC[1], emailCC[1]));
                }
                if (emails.Count() > 0)
                {
                    msg.AddCcs(emails);
                }
            }
            else if (!string.IsNullOrEmpty(emailCC[0]))
            {
                if (emailCC[0] != emailAdd)
                {
                    msg.AddCc(new EmailAddress(emailCC[0], emailCC[0]));
                }
            }
            var response = await client.SendEmailAsync(msg);


        }

    }
}
