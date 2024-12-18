using CarRental_Max.Entities;
using CarRental_Max.I_Services;
using CarRental_Max.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_Max.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackDto feedbackDto)
        {
            await _feedbackService.SubmitFeedbackAsync(feedbackDto.Email, feedbackDto.Message);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedback()
        {
            var feedbacks = await _feedbackService.GetAllFeedbackAsync();
            return Ok(feedbacks);
        }

        [HttpPost("{id}/response")]
        public async Task<IActionResult> SendResponse(int id, [FromBody] ResponseDto responseDto)
        {
            await _feedbackService.SendResponseAsync(id, responseDto.Response);
            return Ok();
        }
    }
}
