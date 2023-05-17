using ReminderAPI.Models.DTOs;

namespace ReminderAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(ReminderDto reminderDto);
    }
}