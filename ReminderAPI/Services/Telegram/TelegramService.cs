using Hangfire;
using ReminderAPI.Helpers;
using ReminderAPI.Models.DTOs;
using Telegram.Bot;

namespace ReminderAPI.Services.Telegram
{
    public class TelegramService : ITelegramService
    {
        public async Task SendTelegramAsync(ReminderDto reminderDto)
        {
            TimeSpan delay = reminderDto.SendAt - DateTime.Now;
            if (delay.TotalSeconds <= 0)
            {
                throw new ArgumentException("Scheduled time must be in the future.");
            }

            Task.Run(async () =>
            {
                await Task.Delay(delay);

                var botClient = new TelegramBotClient(SD.TelegramBotToken);
                await botClient.SendTextMessageAsync(reminderDto.To, reminderDto.Content);
            });
        }
    }
}