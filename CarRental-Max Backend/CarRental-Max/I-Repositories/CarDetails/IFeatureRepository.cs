using CarRental_Max.Entities.CarDetails;

namespace CarRental_Max.I_Repositories.CarDetails
{
    public interface IFeatureRepository
    {
        Task<List<Feature>> GetAllFeaturesAsync();
        Task<Feature> GetFeatureByIdAsync(int id);
        Task AddFeatureAsync(Feature feature);
        Task UpdateFeatureAsync(Feature feature);
        Task DeleteFeatureAsync(int id);
    }
}
