using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;

        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.Include(c => c.Model).Include(c => c.Category).ToListAsync();
        }

        public async Task<bool> CarExistsAsync(int carId)
        {
            return await _context.Cars.AnyAsync(c => c.Id == carId);
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Cars.Include(c => c.Model).Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await GetCarByIdAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }


        // Category

        public async Task<IEnumerable<CarCategory>> GetAllCategoriesAsync()
        {
            return await _context.CarCategories.ToListAsync();
        }

        public async Task<CarCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.CarCategories.FindAsync(id);
        }

        public async Task AddCategoryAsync(CarCategory category)
        {
            _context.CarCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(CarCategory category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            if (category != null)
            {
                _context.CarCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }


        // model 

        public async Task<IEnumerable<Model>> GetAllModelsAsync()
        {
            return await _context.Models.Include(m => m.Category).ToListAsync();
        }

        public async Task<Model> GetModelByIdAsync(int id)
        {
            return await _context.Models.Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddModelAsync(Model model)
        {
            _context.Models.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateModelAsync(Model model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(int id)
        {
            var model = await GetModelByIdAsync(id);
            if (model != null)
            {
                _context.Models.Remove(model);
                await _context.SaveChangesAsync();
            }
        }
    }
}
