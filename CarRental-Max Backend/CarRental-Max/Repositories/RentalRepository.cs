using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;

        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> GetRentalByIdAsync(int rentalId)
        {
            return await _context.Rentals.Include(r => r.Customer).Include(r => r.Car).FirstOrDefaultAsync(r => r.Id == rentalId);
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _context.Rentals.Include(r => r.Customer).Include(r => r.Car).ToListAsync();
        }

        public async Task AddRentalAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RentalExistsAsync(int rentalId)
        {
            return await _context.Rentals.AnyAsync(r => r.Id == rentalId);
        }

        public async Task UpdateRentalAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<Rental>> GetRentalsByCustomerIdAsync(int customerId)
        //{
        //    return await _context.Rentals.Where(r => r.CustomerId == customerId).ToListAsync();
        //}



        public async Task<List<Rental>> GetRentalsByCustomerIdAsync(int customerId)
        {
            return await _context.Rentals
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }


        public async Task<Rental> GetRentalByNicAndCarRegistrationAsync(string nic, string carRegistrationNumber)
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Customer.Nic == nic && r.Car.RegistrationNumber == carRegistrationNumber);
        }





        public async Task<int> GetPendingCountAsync()
        {
            return await _context.Rentals.CountAsync(r => r.Status == RentalStatus.Pending);
        }

        public async Task<int> GetAcceptedCountAsync()
        {
            return await _context.Rentals.CountAsync(r => r.Status == RentalStatus.Accepted);
        }

        public async Task<int> GetRejectedCountAsync()
        {
            return await _context.Rentals.CountAsync(r => r.Status == RentalStatus.Rejected);
        }

        public async Task<int> GetCountByStatusAsync(RentalStatus status)
        {
            return await _context.Rentals.CountAsync(r => r.Status == status);
        }




    }

}
