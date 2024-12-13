using CarRental_Max.Entities.CarDetails;

namespace CarRental_Max.I_Services.CarDetails
{
    public interface ITransmissionService
    {
        Task<List<Transmission>> GetAllTransmissionsAsync();
        Task<Transmission> GetTransmissionByIdAsync(int id);
        Task AddTransmissionAsync(Transmission transmission);
        Task UpdateTransmissionAsync(Transmission transmission);
        Task DeleteTransmissionAsync(int id);
    }
}
