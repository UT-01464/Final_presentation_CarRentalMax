using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Repositories.CarDetails;
using CarRental_Max.I_Services.CarDetails;

namespace CarRental_Max.Services.CarDetails
{
    public class FeatureService:IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public async Task<List<Feature>> GetAllFeaturesAsync()
        {
            return await _featureRepository.GetAllFeaturesAsync();
        }

        public async Task<Feature> GetFeatureByIdAsync(int id)
        {
            return await _featureRepository.GetFeatureByIdAsync(id);
        }

        public async Task AddFeatureAsync(Feature feature)
        {
            await _featureRepository.AddFeatureAsync(feature);
        }

        public async Task UpdateFeatureAsync(Feature feature)
        {
            await _featureRepository.UpdateFeatureAsync(feature);
        }

        public async Task DeleteFeatureAsync(int id)
        {
            await _featureRepository.DeleteFeatureAsync(id);
        }
    }
}
