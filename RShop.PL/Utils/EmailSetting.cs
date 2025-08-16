using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RShop.PL.Utils
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string messageHtml)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("raheeqmousa00@gmail.com", "najt wosl idfc kvqu")
            };

            return client.SendMailAsync(
                new MailMessage(from: "raheeqmousa00@gmail.com",
                                to: email,
                                subject,
                                messageHtml
                                )
                                { IsBodyHtml=true});
        }
    }


}
