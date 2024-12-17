using CAR_RENTAL_MS_III.Entities;

namespace CarRental_Max.I_Repositories
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsByCustomerIdAsync(int customerId);
    }
}
