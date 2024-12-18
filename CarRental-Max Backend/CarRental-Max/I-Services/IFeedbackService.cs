using CarRental_Max.Entities;

namespace CarRental_Max.I_Services
{
    public interface IFeedbackService
    {
        Task SubmitFeedbackAsync(string email, string message);
        Task<IEnumerable<Feedback>> GetAllFeedbackAsync();
        Task SendResponseAsync(int feedbackId, string response);
    }
}
