using CarRental_Max.Entities.CarDetails;

namespace CarRental_Max.I_Repositories.CarDetails
{
    public interface IFuelTypeRepository
    {
        Task<List<FuelType>> GetAllFuelTypesAsync();
        Task<FuelType> GetFuelTypeByIdAsync(int id);
        Task AddFuelTypeAsync(FuelType fuelType);
        Task UpdateFuelTypeAsync(FuelType fuelType);
        Task DeleteFuelTypeAsync(int id);
    }
}
