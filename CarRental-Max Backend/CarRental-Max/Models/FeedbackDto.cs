using System.ComponentModel.DataAnnotations;

namespace CarRental_Max.Models
{
    public class FeedbackDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; }
    }
}
