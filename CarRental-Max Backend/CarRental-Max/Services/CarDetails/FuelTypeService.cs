using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Repositories.CarDetails;
using CarRental_Max.I_Services.CarDetails;

namespace CarRental_Max.Services.CarDetails
{
    public class FuelTypeService:IFuelTypeService
    {
        private readonly IFuelTypeRepository _fuelTypeRepository;

        public FuelTypeService(IFuelTypeRepository fuelTypeRepository)
        {
            _fuelTypeRepository = fuelTypeRepository;
        }

        public async Task<List<FuelType>> GetAllFuelTypesAsync()
        {
            return await _fuelTypeRepository.GetAllFuelTypesAsync();
        }

        public async Task<FuelType> GetFuelTypeByIdAsync(int id)
        {
            return await _fuelTypeRepository.GetFuelTypeByIdAsync(id);
        }

        public async Task AddFuelTypeAsync(FuelType fuelType)
        {
            await _fuelTypeRepository.AddFuelTypeAsync(fuelType);
        }

        public async Task UpdateFuelTypeAsync(FuelType fuelType)
        {
            await _fuelTypeRepository.UpdateFuelTypeAsync(fuelType);
        }

        public async Task DeleteFuelTypeAsync(int id)
        {
            await _fuelTypeRepository.DeleteFuelTypeAsync(id);
        }
    }
}
