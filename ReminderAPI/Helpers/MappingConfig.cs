using AutoMapper;
using ReminderAPI.Models;
using ReminderAPI.Models.DTOs;

namespace ReminderAPI.Helpers
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>{
                config.CreateMap<Reminder, ReminderDto>().ReverseMap();
            });
            return mappingConfig;
        } 
    }
}