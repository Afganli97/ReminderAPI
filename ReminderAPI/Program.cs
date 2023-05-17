using System.Net;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ReminderAPI.DB;
using ReminderAPI.Helpers;
using ReminderAPI.Repositories;
using ReminderAPI.Services;
using ReminderAPI.Services.Email;
using ReminderAPI.Services.Telegram;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

SD.TelegramBotToken = config["TelegramSettings:BotToken"];
SD.TelegramBotId = config["TelegramSettings:BotId"];


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option=>{
    option.UseSqlServer(config.GetConnectionString("Windows"));
});

builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITelegramService, TelegramService>();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
