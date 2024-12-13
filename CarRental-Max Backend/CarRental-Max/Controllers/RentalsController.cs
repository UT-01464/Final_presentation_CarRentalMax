using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Services;
using CarRental_Max.Models.Rentals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return rentalDto != null ? Ok(rentalDto) : NotFound("Rental not found.");
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
            try
            {
                var rentalDetails = await _rentalService.GetRentalDetailsByNicAsync(nic);
                return Ok(rentalDetails);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log unexpected exceptions
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        [HttpPost("rent")]
       
        public async Task<IActionResult> RentCar([FromBody] RentCarDto rentCarDto)
        {
            if (rentCarDto == null)
            {
                return BadRequest("Rental request cannot be null.");
            }

            if (rentCarDto.CustomerId <= 0 || rentCarDto.CarId <= 0)
            {
                return BadRequest("Invalid customer or car ID.");
            }

            try
            {
                await _rentalService.RentCarAsync(rentCarDto.CustomerId, rentCarDto.CarId);

                // Return a JSON object
                return Ok(new { message = "Rental created successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnCar([FromQuery] string nic, [FromQuery] string carRegistrationNumber)
        {
            if (string.IsNullOrWhiteSpace(nic))
            {
                return BadRequest("Customer NIC is required.");
            }

            if (string.IsNullOrWhiteSpace(carRegistrationNumber))
            {
                return BadRequest("Car registration number is required.");
            }

            var result = await _rentalService.ReturnCarByNicAndRegistrationAsync(nic, carRegistrationNumber);
            return result.StartsWith("Car returned successfully") ? Ok(result) : NotFound(result);
        }



        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetPendingRentals()
        {
            var rentals = await _rentalService.GetPendingRentalsAsync();
            return Ok(rentals);
        }









        [HttpPost("accept/{rentalId}")]
        public async Task<IActionResult> AcceptRental(int rentalId)
        {
            var result = await _rentalService.AcceptRentalAsync(rentalId);
            return result == "Rental not found." ? NotFound(result) : Ok(result);
        }

        [HttpPost("reject/{rentalId}")]
        public async Task<IActionResult> RejectRental(int rentalId)
        {
            var result = await _rentalService.RejectRentalAsync(rentalId);
            return result == "Rental not found." ? NotFound(result) : Ok(result);
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
            if (extendRentalDto == null || extendRentalDto.RentalId <= 0 || extendRentalDto.NewReturnDate <= DateTime.UtcNow)
            {
                return BadRequest("Invalid extension request.");
            }

            var result = await _rentalService.ExtendRentalPeriodAsync(extendRentalDto);
            return Ok(result);
        }

        [HttpPost("update-overdue-fee")]
        public async Task<IActionResult> UpdateOverdueFee([FromBody] UpdateOverdueFeeDto updateOverdueFeeDto)
        {
            if (updateOverdueFeeDto == null)
            {
                return BadRequest("Invalid overdue fee update request.");
            }

            var result = await _rentalService.UpdateOverdueFeeAsync(updateOverdueFeeDto);
            return Ok(result);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueCars([FromQuery] decimal overdueFeePerDay)
        {
            var overdueCars = await _rentalService.GetOverdueCarsAsync(overdueFeePerDay);
            return Ok(overdueCars);
        }





        [HttpGet("pending/count")]
        public async Task<ActionResult<int>> GetPendingCount()
        {
            var count = await _rentalService.GetPendingCountAsync();
            return Ok(count);
        }

        
        [HttpGet("accepted/count")]
        public async Task<ActionResult<int>> GetAcceptedCount()
        {
            var count = await _rentalService.GetAcceptedCountAsync();
            return Ok(count);
        }

       
        [HttpGet("rejected/count")]
        public async Task<ActionResult<int>> GetRejectedCount()
        {
            var count = await _rentalService.GetRejectedCountAsync();
            return Ok(count);
        }








    }
}