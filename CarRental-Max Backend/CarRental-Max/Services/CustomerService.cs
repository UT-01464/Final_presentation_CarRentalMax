using Azure.Identity;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CarRental_Max.Entities;
using CarRental_Max.Models.Customer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;

namespace CAR_RENTAL_MS_III.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;


        public CustomerService(ICustomerRepository customerRepository, IConfiguration configuration, IOptions<JwtSettings> jwtSettings)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
            _jwtSettings = jwtSettings.Value;

        }


        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var customer = new Customer
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DriverLicenseNumber = registerDto.DriverLicenseNumber,
                Nic = registerDto.Nic,
                PasswordHash = passwordHash,
                RoleId = (int)Role.Customer,
                //Address = new Address
                //{
                //    Street = registerDto.Address.Street,
                //    City = registerDto.Address.City,
                //    State = registerDto.Address.State,
                //    ZipCode = registerDto.Address.ZipCode,
                //    Country = registerDto.Address.Country
                //}
            };

            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var customer = await _customerRepository.GetCustomerByNicAsync(loginDto.Nic);
            if (customer == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, customer.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return GenerateToken(customer);
        }



        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Nic = c.Nic,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            });
        }






        public async Task<CustomerDto> GetCustomerByNicAsync(string nic)
        {
            var customer = await _customerRepository.GetCustomerByNicAsync(nic);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                DriverLicenseNumber = customer.DriverLicenseNumber,
                Nic = customer.Nic,
                Address = new AddressDto
                {
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    State = customer.Address.State,
                    ZipCode = customer.Address.ZipCode,
                    Country = customer.Address.Country
                }
            };
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                DriverLicenseNumber = customer.DriverLicenseNumber,
                Nic = customer.Nic,
                Address = new AddressDto
                {
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    State = customer.Address.State,
                    ZipCode = customer.Address.ZipCode,
                    Country = customer.Address.Country
                }
            };
        }

        public async Task UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerDto.Id);
            if (customer == null) throw new Exception("Customer not found");

            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.Email = customerDto.Email;
            customer.PhoneNumber = customerDto.PhoneNumber;
            customer.DriverLicenseNumber = customerDto.DriverLicenseNumber;
            customer.Nic = customerDto.Nic;

            // Update address
            customer.Address.Street = customerDto.Address.Street;
            customer.Address.City = customerDto.Address.City;
            customer.Address.State = customerDto.Address.State;
            customer.Address.ZipCode = customerDto.Address.ZipCode;
            customer.Address.Country = customerDto.Address.Country;

            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }




        private string GenerateToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
            new Claim(ClaimTypes.Name, customer.FirstName + " " + customer.LastName),
            new Claim(ClaimTypes.Role, customer.Role.ToString()) 
        }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
