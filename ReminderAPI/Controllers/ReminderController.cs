using Microsoft.AspNetCore.Mvc;
using ReminderAPI.DB;
using ReminderAPI.Models;
using ReminderAPI.Models.DTOs;
using ReminderAPI.Repositories;
using ReminderAPI.Services;
using ReminderAPI.Services.Telegram;

namespace ReminderAPI.Controllers
{
    [ApiController]
    [Route("api/reminders")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderController(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReminder(ReminderDto reminderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reminderRepository.CreateReminderAsync(reminderDto);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReminders()
        {
            var reminderDtos = await _reminderRepository.GetAllRemindersAsync();

            return Ok(reminderDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReminderById(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var reminderDto = await _reminderRepository.GetReminderByIdAsync(id);

            return Ok(reminderDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReminder(int? id, ReminderDto reminderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id == null || reminderDto == null) 
            {
                return BadRequest();
            }

            var result = await _reminderRepository.UpdateReminderAsync(id, reminderDto);

            if(!result)
            {
                return NotFound();
            }
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var result = await _reminderRepository.DeleteReminderAsync(id);

            if(!result)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}