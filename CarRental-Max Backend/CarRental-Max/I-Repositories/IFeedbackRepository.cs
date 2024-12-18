using CarRental_Max.Entities;

namespace CarRental_Max.I_Repositories
{
    public interface IFeedbackRepository
    {
        Task AddFeedbackAsync(Feedback feedback);
        Task<IEnumerable<Feedback>> GetAllFeedbackAsync();
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task UpdateFeedbackAsync(Feedback feedback);
        Task DeleteFeedbackAsync(int id);
    }
}
