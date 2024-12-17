using CAR_RENTAL_MS_III.Entities;

namespace CarRental_Max.I_Services
{
    public interface INotificationService
    {
        Task NotifyCustomerAsync(int customerId, string message, int? rentalId = null);
        Task<IEnumerable<Notification>> GetNotificationsAsync(int customerId);
    }
}
