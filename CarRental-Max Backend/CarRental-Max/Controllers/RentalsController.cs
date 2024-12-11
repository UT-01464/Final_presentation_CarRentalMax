
using CAR_RENTAL_MS_III.Services;
using CarRental_Max.Models.Rentals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {


        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("{rentalId}")]
        public async Task<IActionResult> GetRental(int rentalId)
        {
            var rentalDto = await _rentalService.GetRentalByIdAsync(rentalId);
            return rentalDto != null ? Ok(rentalDto) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentals()
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return Ok(rentals);
        }

        [HttpGet("details/{nic}")]
        public async Task<IActionResult> GetRentalDetailsByNic(string nic)
        {
            var rentalDetails = await _rentalService.GetRentalDetailsByNicAsync(nic);
            return rentalDetails != null ? Ok(rentalDetails) : NotFound();
        }

        [HttpPost("rent")]
        public async Task<IActionResult> RentCar([FromBody] RentCarDto rentCarDto)
        {
            await _rentalService.RentCarAsync(rentCarDto.CustomerId, rentCarDto.CarId);
            return Ok("Car rental request submitted.");
        }
        [HttpPost("return")]
        public async Task<IActionResult> ReturnCar([FromQuery] string nic, [FromQuery] string carRegistrationNumber)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(nic))
            {
                return BadRequest("Customer NIC is required.");
            }

            if (string.IsNullOrWhiteSpace(carRegistrationNumber))
            {
                return BadRequest("Car registration number is required.");
            }

            // Call the service method to return the car
            var result = await _rentalService.ReturnCarByNicAndRegistrationAsync(nic, carRegistrationNumber);

            // Return appropriate response
            if (result.StartsWith("Car returned successfully"))
            {
                return Ok(result); // 200 OK
            }
            else
            {
                return NotFound(result); // 404 Not Found or Bad Request
            }
        }

        [HttpPost("accept/{rentalId}")]
        public async Task<IActionResult> AcceptRental(int rentalId)
        {
            var result = await _rentalService.AcceptRentalAsync(rentalId);
            return Ok(result);
        }

        [HttpPost("reject/{rentalId}")]
        public async Task<IActionResult> RejectRental(int rentalId)
        {
            var result = await _rentalService.RejectRentalAsync(rentalId);
            return Ok(result);
        }

        [HttpGet("history/{customerId}")]
        public async Task<IActionResult> GetRentalHistory(int customerId)
        {
            var rentals = await _rentalService.GetRentalHistoryByCustomerIdAsync(customerId);
            return Ok(rentals);
        }






        [HttpPost("extend")]
        public async Task<IActionResult> ExtendRentalPeriod([FromBody] ExtendRentalDto extendRentalDto)
        {
            var result = await _rentalService.ExtendRentalPeriodAsync(extendRentalDto);
            return Ok(result);
        }

        

        [HttpPost("update-overdue-fee")]
        public async Task<IActionResult> UpdateOverdueFee([FromBody] UpdateOverdueFeeDto updateOverdueFeeDto)
        {
            var result = await _rentalService.UpdateOverdueFeeAsync(updateOverdueFeeDto);
            return Ok(result);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueCars([FromQuery] decimal overdueFeePerDay)
        {
            var overdueCars = await _rentalService.GetOverdueCarsAsync(overdueFeePerDay);
            return Ok(overdueCars);
        }

    }
}
