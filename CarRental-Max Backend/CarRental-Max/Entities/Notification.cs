using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Notification
    {
        public int Id { get; set; } 
        public int CustomerId { get; set; } 
        public int? RentalId { get; set; } 
        public string Message { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public bool IsRead { get; set; } 

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Rental Rental { get; set; }
    }
}
