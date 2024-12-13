using CAR_RENTAL_MS_III.Entities;
using CarRental_Max.Models.Rentals;

namespace CAR_RENTAL_MS_III.Services
{
    public interface IRentalService
    {
        Task<RentalDto> GetRentalByIdAsync(int rentalId);
        Task<IEnumerable<RentalDto>> GetAllRentalsAsync();
        //Task<RentalDetailsDto> GetRentalDetailsByNicAsync(string nic);
        Task<RentalDetailsDto> GetRentalDetailsByNicAsync(string nic);
        Task RentCarAsync(int customerId, int carId);
        Task<string> ReturnCarByNicAndRegistrationAsync(string nic, string carRegistrationNumber);
        Task<string> AcceptRentalAsync(int rentalId);
        Task<string> RejectRentalAsync(int rentalId);
        Task<IEnumerable<RentalDto>> GetRentalHistoryByCustomerIdAsync(int customerId);


        Task<IEnumerable<RentalDto>> GetPendingRentalsAsync();

        Task<string> ExtendRentalPeriodAsync(ExtendRentalDto extendRentalDto);
        Task<IEnumerable<OverdueCarDto>> GetOverdueCarsAsync(decimal overdueFeePerDay);
        Task<string> UpdateOverdueFeeAsync(UpdateOverdueFeeDto updateOverdueFeeDto);



        Task<int> GetPendingCountAsync();
        Task<int> GetAcceptedCountAsync();
        Task<int> GetRejectedCountAsync();



        
    }
}
