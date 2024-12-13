using CAR_RENTAL_MS_III.Data;
using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Repositories.CarDetails;
using Microsoft.EntityFrameworkCore;

namespace CarRental_Max.Repositories.CarDetails
{
    public class FuelTypeRepository:IFuelTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public FuelTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FuelType>> GetAllFuelTypesAsync()
        {
            return await _context.FuelTypes.ToListAsync();
        }

        public async Task<FuelType> GetFuelTypeByIdAsync(int id)
        {
            return await _context.FuelTypes.FindAsync(id);
        }

        public async Task AddFuelTypeAsync(FuelType fuelType)
        {
            _context.FuelTypes.Add(fuelType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFuelTypeAsync(FuelType fuelType)
        {
            _context.FuelTypes.Update(fuelType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFuelTypeAsync(int id)
        {
            var fuelType = await GetFuelTypeByIdAsync(id);
            if (fuelType != null)
            {
                _context.FuelTypes.Remove(fuelType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
