using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CAR_RENTAL_MS_III.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the car

        public decimal PricePerDay { get; set; }
        public int Year { get; set; } // Model year
        public string RegistrationNumber { get; set; } // Unique registration number
        
        public bool IsAvailable { get; set; } // Availability status
        public string ImageUrl { get; set; } // URL of the car image


        [ForeignKey("Model")] // Explicitly define the foreign key relationship
        public int ModelId { get; set; } // Foreign key to Model
        public virtual Model Model { get; set; } // Navigation property for Model


        [ForeignKey("CarCategory")]
        public int CategoryId { get; set; } // Category of the car
        public virtual CarCategory Category { get; set; } // Navigation property for Category

        public ICollection<Rental>Rentals { get; set; }



    }
}
