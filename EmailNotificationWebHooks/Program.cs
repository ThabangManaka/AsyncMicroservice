using EmailNotificationWebHooks.Consumer;
using EmailNotificationWebHooks.Service;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<IEmailService, EmailService>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<WebhookConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });
        cfg.ReceiveEndpoint("email-webhook-queue", e =>
        {
            e.ConfigureConsumer<WebhookConsumer>(context);
        });
    });
});
var app = builder.Build();

app.MapGet("/email-webhook", ([FromBody] EmailDTO email, IEmailService emailService) =>
{
    var result = emailService.SendEamil(email);
    return Task.FromResult(result);  
});

app.Run();
