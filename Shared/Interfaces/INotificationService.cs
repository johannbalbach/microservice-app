using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace Shared.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(MailData message);
    }
}
