using CAR_RENTAL_MS_III.Data;
using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Repositories.CarDetails;
using Microsoft.EntityFrameworkCore;

namespace CarRental_Max.Repositories.CarDetails
{
    public class FeatureRepository:IFeatureRepository
    {
        private readonly ApplicationDbContext _context;

        public FeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Feature>> GetAllFeaturesAsync()
        {
            return await _context.Features.ToListAsync();
        }

        public async Task<Feature> GetFeatureByIdAsync(int id)
        {
            return await _context.Features.FindAsync(id);
        }

        public async Task AddFeatureAsync(Feature feature)
        {
            _context.Features.Add(feature);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeatureAsync(Feature feature)
        {
            _context.Features.Update(feature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeatureAsync(int id)
        {
            var feature = await GetFeatureByIdAsync(id);
            if (feature != null)
            {
                _context.Features.Remove(feature);
                await _context.SaveChangesAsync();
            }
        }
    }
}
