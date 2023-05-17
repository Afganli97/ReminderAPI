using Microsoft.EntityFrameworkCore;
using ReminderAPI.Models;

namespace ReminderAPI.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Reminder> Reminders { get; set; }
    }
}