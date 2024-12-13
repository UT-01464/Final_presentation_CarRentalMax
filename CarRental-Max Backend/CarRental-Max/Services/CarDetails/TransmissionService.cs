using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Repositories.CarDetails;
using CarRental_Max.I_Services.CarDetails;

namespace CarRental_Max.Services.CarDetails
{
    public class TransmissionService: ITransmissionService
    {
        public readonly ITransmissionRepository _transmissionRepository;

        public TransmissionService(ITransmissionRepository transmissionRepository)
        {
            _transmissionRepository = transmissionRepository;
        }

        public async Task<List<Transmission>> GetAllTransmissionsAsync()
        {
            return await _transmissionRepository.GetAllTransmissionsAsync();
        }

        public async Task<Transmission> GetTransmissionByIdAsync(int id)
        {
            return await _transmissionRepository.GetTransmissionByIdAsync(id);
        }

        public async Task AddTransmissionAsync(Transmission transmission)
        {
            await _transmissionRepository.AddTransmissionAsync(transmission);
        }

        public async Task UpdateTransmissionAsync(Transmission transmission)
        {
            await _transmissionRepository.UpdateTransmissionAsync(transmission);
        }

        public async Task DeleteTransmissionAsync(int id)
        {
            await _transmissionRepository.DeleteTransmissionAsync(id);
        }
    }
}
