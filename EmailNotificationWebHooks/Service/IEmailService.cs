using Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EmailNotificationWebHooks.Service
{
    public interface IEmailService
    {
        string SendEamil(EmailDTO emailDTo);
    }
}
