using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CarRental_Max.I_Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRental_Max.Repositories
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByCustomerIdAsync(int customerId)
        {
            return await _context.Notifications
                .Where(n => n.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
