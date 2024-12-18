using CarRental_Max.Entities;
using CarRental_Max.I_Repositories;
using CarRental_Max.I_Services;

namespace CarRental_Max.Services
{
    public class FeedbackService:IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task SubmitFeedbackAsync(string email, string message)
        {
            var feedback = new Feedback
            {
                Email = email,
                Message = message,
                AdminResponse = string.Empty 
            };
            await _feedbackRepository.AddFeedbackAsync(feedback);
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
        {
            return await _feedbackRepository.GetAllFeedbackAsync();
        }

        public async Task SendResponseAsync(int feedbackId, string response)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(feedbackId);
            if (feedback != null)
            {
                feedback.AdminResponse = response;
                feedback.IsResponded = true;
                await _feedbackRepository.UpdateFeedbackAsync(feedback);
                
            }
        }
    }
}
