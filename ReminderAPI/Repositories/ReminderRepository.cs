using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReminderAPI.DB;
using ReminderAPI.Models;
using ReminderAPI.Models.DTOs;
using ReminderAPI.Services;
using ReminderAPI.Services.Telegram;

namespace ReminderAPI.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly ILogger<ReminderRepository> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITelegramService _telegramService;
        private readonly IEmailService _emailService;

        public ReminderRepository(AppDbContext dbContext, IMapper mapper, ILogger<ReminderRepository> logger, ITelegramService telegramService, IEmailService emailService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _telegramService = telegramService;
            _emailService = emailService;
        }

        public async Task<List<ReminderDto>> GetAllRemindersAsync()
        {
            try
            {
                var reminders = await _dbContext.Reminders.ToListAsync();
                var remindersDto = _mapper.Map<List<ReminderDto>>(reminders);
                return remindersDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReminderDto> GetReminderByIdAsync(int? id)
        {
            try
            {
                var reminder = await _dbContext.Reminders.FindAsync(id);
                var reminderDto = _mapper.Map<ReminderDto>(reminder);
                return reminderDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateReminderAsync(ReminderDto reminderDto)
        {
            try
            {
                if (reminderDto.Method.ToLower() == "telegram")
                {
                    await _telegramService.SendTelegramAsync(reminderDto);
                }
                else if(reminderDto.Method.ToLower() == "email")
                {
                    await _emailService.SendEmailAsync(reminderDto);
                }
                else
                {
                    return false;
                }
                var reminder = _mapper.Map<Reminder>(reminderDto);
                _dbContext.Reminders.Add(reminder);
                await _dbContext.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CreateReminderAsync");        
                return false;
            }
        }

        public async Task<bool> UpdateReminderAsync(int? id, ReminderDto reminderDto)
        {
            var reminder = await _dbContext.Reminders.FindAsync(id);
            if (reminder == null)
            {
                throw new Exception("Reminder not found");
            }

            try
            {
                reminder = _mapper.Map<Reminder>(reminderDto);
                await _dbContext.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CreateReminderAsync");        
                return false;
            }
        }

        public async Task<bool> DeleteReminderAsync(int? id)
        {
            var reminder = await _dbContext.Reminders.FindAsync(id);

            if (reminder == null)
            {
                throw new Exception("Reminder not found");
            }

            try
            {
                _dbContext.Reminders.Remove(reminder);
                await _dbContext.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CreateReminderAsync");        
                return false;
            }
        }
    }
}