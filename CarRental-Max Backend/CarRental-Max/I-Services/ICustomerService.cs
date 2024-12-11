using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models;
using CarRental_Max.Models.Customer;

namespace CAR_RENTAL_MS_III.I_Services
{
    public interface ICustomerService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<CustomerDto> GetCustomerByNicAsync(string nic);
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(CustomerDto customerDto);
        Task DeleteCustomerAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();


    }
}
