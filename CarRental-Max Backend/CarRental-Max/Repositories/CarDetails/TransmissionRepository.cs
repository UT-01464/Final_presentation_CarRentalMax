using CAR_RENTAL_MS_III.Data;
using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Repositories.CarDetails;
using Microsoft.EntityFrameworkCore;

namespace CarRental_Max.Repositories.CarDetails
{
    public class TransmissionRepository: ITransmissionRepository
    {
        public readonly ApplicationDbContext _context;

        public TransmissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transmission>> GetAllTransmissionsAsync()
        {
            return await _context.Transmissions.ToListAsync();
        }

        public async Task<Transmission> GetTransmissionByIdAsync(int id)
        {
            return await _context.Transmissions.FindAsync(id);
        }

        public async Task AddTransmissionAsync(Transmission transmission)
        {
            _context.Transmissions.Add(transmission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransmissionAsync(Transmission transmission)
        {
            _context.Transmissions.Update(transmission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransmissionAsync(int id)
        {
            var transmission = await GetTransmissionByIdAsync(id);
            if (transmission != null)
            {
                _context.Transmissions.Remove(transmission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
