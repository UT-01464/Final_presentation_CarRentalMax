using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Services.CarDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_Max.Controllers.CarDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransmissionsController : ControllerBase
    {
        private readonly ITransmissionService _transmissionService;

        public TransmissionsController(ITransmissionService transmissionService)
        {
            _transmissionService = transmissionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Transmission>>> GetAllTransmissions()
        {
            return await _transmissionService.GetAllTransmissionsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transmission>> GetTransmission(int id)
        {
            var transmission = await _transmissionService.GetTransmissionByIdAsync(id);
            if (transmission == null)
                return NotFound();
            return transmission;
        }

        [HttpPost]
        public async Task<ActionResult> AddTransmission(Transmission transmission)
        {
            await _transmissionService.AddTransmissionAsync(transmission);
            return CreatedAtAction(nameof(GetTransmission), new { id = transmission.Id }, transmission);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransmission(int id, Transmission transmission)
        {
            if (id != transmission.Id)
                return BadRequest();

            await _transmissionService.UpdateTransmissionAsync(transmission);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransmission(int id)
        {
            await _transmissionService.DeleteTransmissionAsync(id);
            return NoContent();
        }
    }
}
