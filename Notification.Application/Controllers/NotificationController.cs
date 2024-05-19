using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Interfaces;
using static MassTransit.ValidationResultExtensions;

namespace Notification.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(MailData mailData)
        {
            var result = await _notificationService.SendNotificationAsync(mailData);

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }
    }
}
