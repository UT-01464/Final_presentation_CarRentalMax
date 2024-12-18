using CAR_RENTAL_MS_III.Data;
using CarRental_Max.Entities;
using CarRental_Max.I_Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRental_Max.Repositories
{
    public class FeedbackRepository: IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await GetFeedbackByIdAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
