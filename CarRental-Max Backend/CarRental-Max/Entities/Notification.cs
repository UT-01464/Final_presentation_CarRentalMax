using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Notification
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Rental")]
        public int RentalId { get; set; }
        public Rental Rental { get; set; }

        public string Type { get; set; } 

        public string Comments { get; set; }
    }
}
