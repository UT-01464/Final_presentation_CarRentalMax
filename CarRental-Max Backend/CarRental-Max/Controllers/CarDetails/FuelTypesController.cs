using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Services.CarDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_Max.Controllers.CarDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelTypesController : ControllerBase
    {
        private readonly IFuelTypeService _fuelTypeService;

        public FuelTypesController(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FuelType>>> GetAllFuelTypes()
        {
            return await _fuelTypeService.GetAllFuelTypesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuelType>> GetFuelType(int id)
        {
            var fuelType = await _fuelTypeService.GetFuelTypeByIdAsync(id);
            if (fuelType == null)
                return NotFound();
            return fuelType;
        }

        [HttpPost]
        public async Task<ActionResult> AddFuelType(FuelType fuelType)
        {
            await _fuelTypeService.AddFuelTypeAsync(fuelType);
            return CreatedAtAction(nameof(GetFuelType), new { id = fuelType.Id }, fuelType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFuelType(int id, FuelType fuelType)
        {
            if (id != fuelType.Id)
                return BadRequest();

            await _fuelTypeService.UpdateFuelTypeAsync(fuelType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFuelType(int id)
        {
            await _fuelTypeService.DeleteFuelTypeAsync(id);
            return NoContent();
        }
    }
}
