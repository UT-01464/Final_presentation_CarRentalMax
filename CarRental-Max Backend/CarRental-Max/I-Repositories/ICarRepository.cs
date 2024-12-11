using CAR_RENTAL_MS_III.Entities;

namespace CAR_RENTAL_MS_III.I_Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);



        // Category

        Task<IEnumerable<CarCategory>> GetAllCategoriesAsync();
        Task<CarCategory> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CarCategory category);
        Task UpdateCategoryAsync(CarCategory category);
        Task DeleteCategoryAsync(int id);


        // Model 

        Task<IEnumerable<Model>> GetAllModelsAsync();
        Task<Model> GetModelByIdAsync(int id);
        Task AddModelAsync(Model model);
        Task UpdateModelAsync(Model model);
        Task DeleteModelAsync(int id);
    }
}
