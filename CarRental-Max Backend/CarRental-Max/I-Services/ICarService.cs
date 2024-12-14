using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models.Car;
using CarRental_Max.Models.Car;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.I_Services
{
    public interface ICarService
    {

        // Car-related methods
        Task<IEnumerable<CarDto>> GetAllCarsAsync();
        Task<CarDto> GetCarByIdAsync(int id);
        Task AddCarAsync(CarDto carDto);
        Task UpdateCarAsync(CarDto carDto);
        Task DeleteCarAsync(int id);
        Task<CarDto> GetCarByRegistrationNumberAsync(string registrationNumber);

        // Category-related methods
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);

        // Model-related methods
        Task<IEnumerable<ModelDto>> GetAllModelsAsync();
        Task<ModelDto> GetModelByIdAsync(int id);
        Task AddModelAsync(ModelDto modelDto);
        Task UpdateModelAsync(ModelDto modelDto);
        Task DeleteModelAsync(int id);

    }
}
