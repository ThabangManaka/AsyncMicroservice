using EmailNotificationWebHooks.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IEmailService, EmailService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
