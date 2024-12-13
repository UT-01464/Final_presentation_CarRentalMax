using CarRental_Max.Entities.CarDetails;

namespace CarRental_Max.I_Services.CarDetails
{
    public interface IFuelTypeService
    {
        Task<List<FuelType>> GetAllFuelTypesAsync();
        Task<FuelType> GetFuelTypeByIdAsync(int id);
        Task AddFuelTypeAsync(FuelType fuelType);
        Task UpdateFuelTypeAsync(FuelType fuelType);
        Task DeleteFuelTypeAsync(int id);
    }
}
