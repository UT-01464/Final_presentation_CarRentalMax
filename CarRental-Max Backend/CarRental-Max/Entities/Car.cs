using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarRental_Max.Entities.CarDetails;


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
        public int TransmissionId { get; set; } // Ensure this property exists
        public int FuelTypeId { get; set; } // Ensure this property exists
       
        

        [ForeignKey("Model")] // Explicitly define the foreign key relationship
        public int ModelId { get; set; } // Foreign key to Model
        public virtual Model Model { get; set; } // Navigation property for Model


        [ForeignKey("CarCategory")]
        public int CategoryId { get; set; } // Category of the car
        public virtual CarCategory Category { get; set; } // Navigation property for Category

        public ICollection<Rental>Rentals { get; set; }


        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public virtual Transmission Transmission { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
       


    }
}
