using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using CarRental_Max.Models.Customer;
using Microsoft.AspNetCore.Mvc;


namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            await _customerService.RegisterAsync(registerDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            var token = await _customerService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }


        [HttpGet("GetAllCustomers")] // New endpoint to get all customers
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }





        [HttpGet("GetCustomerByNic/{nic}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByNic(string nic)
        {
            var customer = await _customerService.GetCustomerByNicAsync(nic);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet("GetCustomerById/{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (id != customerDto.Id) return BadRequest();
            await _customerService.UpdateCustomerAsync(customerDto);
            return NoContent();
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }


    }
}
