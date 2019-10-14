using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E4S.Services
{
  public interface IEmailSender
  {
    Task SendEmailAsync(string email, string subject, string message);
    string SendPlainEmailAsync(string emailAdd, string subject, string message);

    string SendLinkEmailAsync(string emailAdd, string subject, string message);

    Task SendGridEmailAsync(string emailAdd, string subject, string message, string organisation, string firstname, string template);

    Task SendGridEmailConfrimationAsync(string emailAdd, string subject, string message, string firstname);

        Task SendGridEmailWithAttachmentAsync(string emailAdd, string[] emailCC, string subject, string message, string base64string, string fileName);
    }
}
