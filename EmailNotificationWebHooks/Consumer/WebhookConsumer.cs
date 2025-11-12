using MassTransit;
using Shared.DTOs;

namespace EmailNotificationWebHooks.Consumer
{
    public class WebhookConsumer(HttpClient client) : IConsumer<EmailDTO>
    {

        public async Task Consume(ConsumeContext<EmailDTO> context)
        {
            var emailDto = context.Message;
            var response = await client.PostAsJsonAsync("https://localhost:7080/emai-webhook",
                new EmailDTO(context.Message.Title, context.Message.Content));
            response.EnsureSuccessStatusCode();
        }
    }

}