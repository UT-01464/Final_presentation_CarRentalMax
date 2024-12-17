using CAR_RENTAL_MS_III.Entities;
using CarRental_Max.I_Repositories;
using CarRental_Max.I_Services;

namespace CarRental_Max.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

       

        public async Task NotifyCustomerAsync(int customerId, string message, int? rentalId = null)
        {
            var notification = new Notification
            {
                CustomerId = customerId,
                RentalId = rentalId,
                Message = message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };
            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync(int customerId)
        {
            return await _notificationRepository.GetNotificationsByCustomerIdAsync(customerId);
        }
    }
}
