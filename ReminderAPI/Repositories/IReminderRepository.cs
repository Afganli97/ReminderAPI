using ReminderAPI.Models;
using ReminderAPI.Models.DTOs;

namespace ReminderAPI.Repositories
{
    public interface IReminderRepository
    {
        Task<List<ReminderDto>> GetAllRemindersAsync();
        Task<ReminderDto> GetReminderByIdAsync(int? id);
        Task<bool> CreateReminderAsync(ReminderDto reminderDto);
        Task<bool> UpdateReminderAsync(int? id, ReminderDto reminderDto);
        Task<bool> DeleteReminderAsync(int? id);
    }
}