using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByNicAsync(string nic);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task AddCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
