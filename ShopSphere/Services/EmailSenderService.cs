using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Reactive.Subjects;
using System.Net.Http;
namespace ShopSphere.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private const string sender_adress = "cosminandreiparaschiv@gmail.com";
        private const string sender_password = "hatzgionel1";
            
       

        public async Task<string> SendEmailAsync(string email,string subject,  string body)
        {
            // Set up the email message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sender_adress); 
            mail.To.Add(email); 
            mail.Subject = subject; 
            mail.Body = body;
            

            // Configure the SMTP client
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587, // Usually 587 for TLS, 465 for SSL
            Credentials = new NetworkCredential(sender_adress, sender_password),
            EnableSsl = true,  // Enables secure SSL/TLS connections
        };
            try
            {
                await smtpClient.SendMailAsync(mail);
                return "Mail sent successfully!";
            }
            catch
            {
                return "This email adress doesn't exist.";
            }

        }
    }
}
