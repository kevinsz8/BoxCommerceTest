using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BoxSendNotification.Services
{
    public class SendEmailService
    {
        public async Task SendEmailNotification(string bodyMessage, string Email, string Subject)
        {
            
            string fromAddress = "kevinsz2805@gmail.com";
            string toAddress = Email;
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = Subject,
                Body = bodyMessage,
                IsBodyHtml = true
            };

            mail.To.Add(toAddress);

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp-relay.brevo.com",
                Port = 587,
                Credentials = new NetworkCredential("kevinsz2805@gmail.com", "ULO5jD4yGXvNHkah"),
                EnableSsl = true
            };

            try
            {
                Console.WriteLine("Sending email...");
                await smtp.SendMailAsync(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }

        }
    }
}
