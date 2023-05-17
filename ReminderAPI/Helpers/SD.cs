namespace ReminderAPI.Helpers
{
    public static class SD
    {
        public static string TelegramBotToken { get; set; }
        public static string TelegramBotId { get; set; }

        public enum Methods
        {
            Email,
            Telegram
        }

    }
}