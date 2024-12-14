using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Car;
using CAR_RENTAL_MS_III.Services;
using CarRental_Max.Models.Car;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/cars
        [HttpGet("GetCars")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {
            return Ok(await _carService.GetAllCarsAsync());
        }

        // GET: api/cars/{id}
        [HttpGet("GetCar/{id}")]
        public async Task<ActionResult<CarDto>> GetCar(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        // POST: api/cars
        [HttpPost("AddCar")]
        public async Task<ActionResult<CarDto>> AddCar(CarDto carDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _carService.AddCarAsync(carDto);
            return CreatedAtAction(nameof(GetCar), new { id = carDto.Id }, carDto);
        }

        // PUT: api/cars/{id}
        [HttpPut("UpdateCar/{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarDto carDto)
        {
            if (id != carDto.Id || !ModelState.IsValid) return BadRequest(ModelState);
            await _carService.UpdateCarAsync(carDto);
            return NoContent();
        }

        // DELETE: api/cars/{id}
        [HttpDelete("DeleteCar/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return NoContent();
        }



        // Model 

        [HttpGet("GetCarModels")]
        public async Task<ActionResult<IEnumerable<ModelDto>>> GetCarModels()
        {
            return Ok(await _carService.GetAllModelsAsync());
        }

        // GET: api/carmodels/{id}
        [HttpGet("GetCarModel/{id}")]
        public async Task<ActionResult<ModelDto>> GetCarModel(int id)
        {
            var model = await _carService.GetModelByIdAsync(id);
            if (model == null) return NotFound();
            return Ok(model);
        }

        // POST: api/carmodels
        [HttpPost("AddCarModel")]
        public async Task<ActionResult<ModelDto>> AddCarModel(ModelDto modelDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _carService.AddModelAsync(modelDto);
            return CreatedAtAction(nameof(GetCarModel), new { id = modelDto.Id }, modelDto);
        }

        // PUT: api/carmodels/{id}
        [HttpPut("UpdateCarModel/{id}")]
        public async Task<IActionResult> UpdateCarModel(int id, ModelDto modelDto)
        {
            if (id != modelDto.Id || !ModelState.IsValid) return BadRequest(ModelState);
            await _carService.UpdateModelAsync(modelDto);
            return NoContent();
        }

        // DELETE: api/carmodels/{id}
        [HttpDelete("DeleteCarModel/{id}")]
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            await _carService.DeleteModelAsync(id);
            return NoContent();
        }



        // Categories

        // GET: api/categories
        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            return Ok(await _carService.GetAllCategoriesAsync());
        }

        // GET: api/categories/{id}
        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _carService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST: api/categories
        [HttpPost("AddCategory")]
        public async Task<ActionResult<CategoryDto>> AddCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _carService.AddCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = categoryDto.Id }, categoryDto);
        }

        // PUT: api/categories/{id}
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id || !ModelState.IsValid) return BadRequest(ModelState);
            await _carService.UpdateCategoryAsync(categoryDto);
            return NoContent();
        }

        // DELETE: api/categories/{id}
        [HttpDelete("DeleteCategory{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _carService.DeleteCategoryAsync(id);
            return NoContent();
        }


        [HttpGet("registration/{registrationNumber}")]
        public async Task<IActionResult> GetCarByRegistrationNumber(string registrationNumber)
        {
            var carDto = await _carService.GetCarByRegistrationNumberAsync(registrationNumber);
            return carDto != null ? Ok(carDto) : NotFound("Car not found.");
        }

    }


}
