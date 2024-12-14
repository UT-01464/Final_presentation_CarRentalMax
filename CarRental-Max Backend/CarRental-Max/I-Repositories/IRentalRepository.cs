using CAR_RENTAL_MS_III.Entities;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> GetRentalByIdAsync(int rentalId);
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task AddRentalAsync(Rental rental);
        Task UpdateRentalAsync(Rental rental);
        //Task<IEnumerable<Rental>> GetRentalsByCustomerIdAsync(int customerId);
        Task<List<Rental>> GetRentalsByCustomerIdAsync(int customerId);
        Task<Rental> GetRentalByNicAndCarRegistrationAsync(string nic, string carRegistrationNumber);


        Task<int> GetPendingCountAsync();
        Task<int> GetAcceptedCountAsync();
        Task<int> GetRejectedCountAsync();

        
        Task<bool> RentalExistsAsync(int rentalId);
        Task<int> GetCountByStatusAsync(RentalStatus status);
    }
}
