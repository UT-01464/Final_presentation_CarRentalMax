using CarRental_Max.I_Services;
using CarRental_Max.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_Max.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this._notificationService = notificationService;
        }


        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetNotifications(int customerId)
        {
            var notifications = await _notificationService.GetNotificationsAsync(customerId);
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(int customerId, string message, int? rentalId = null)
        {
            await _notificationService.NotifyCustomerAsync(customerId, message, rentalId);
            return CreatedAtAction(nameof(GetNotifications), new { customerId }, null);
        }





    }
}
