using System.Net;
using System.Net.Mail;
using MailKit.Security;
using MimeKit;
using ReminderAPI.Models;
using ReminderAPI.Models.DTOs;

namespace ReminderAPI.Services.Email
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(ReminderDto reminderDto)
        {
            TimeSpan delay = reminderDto.SendAt - DateTime.Now;
            if (delay.TotalSeconds <= 0)
            {
                throw new ArgumentException("Scheduled time must be in the future.");
            }

            await Task.Delay(delay);

            Task.Run(async () =>
            {
                await Task.Delay(delay);

                MailMessage mailMessage = new();
                mailMessage.From = new MailAddress("Ulviva@code.edu.az", "Afganli Ulvi");
                mailMessage.To.Add(reminderDto.To);
                mailMessage.Body = reminderDto.Content;

                SmtpClient smtpClient = new();
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("ulviva@code.edu.az", "qalmovukuawqvmud");

                smtpClient.Send(mailMessage);
            });
        }
    }
}