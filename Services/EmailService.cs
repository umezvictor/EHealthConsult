using EHealthConsult.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EHealthConsult.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string toEmail, string subject, string body)
        {
            //get email settings
            var emailConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("EmailSettings");

            string address = emailConfig["MailAddress"];
            string senderName = emailConfig["SenderName"];
            string password = emailConfig["MailPassword"];
            string host = emailConfig["Host"];
            string port = emailConfig["Port"];


            MailMessage message = new MailMessage();

            message.To.Add(toEmail);
            message.Subject = (subject);
            message.From = new MailAddress(address, senderName, Encoding.UTF8);
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            smtp.EnableSsl = true;

            NetworkCredential NetworkCred = new NetworkCredential(address, password);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = Convert.ToInt32(port);
            smtp.Send(message);
            
        }
    }
}
