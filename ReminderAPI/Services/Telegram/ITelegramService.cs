using ReminderAPI.Models.DTOs;

namespace ReminderAPI.Services.Telegram
{
    public interface ITelegramService
    {
        Task SendTelegramAsync(ReminderDto reminderDto);
    }
}