using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarRental_Max.Entities.CarDetails;


namespace CAR_RENTAL_MS_III.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; } 

        public decimal PricePerDay { get; set; }
        public int Year { get; set; } 
        public string RegistrationNumber { get; set; } 
        
        public bool IsAvailable { get; set; } 
        public string ImageUrl { get; set; }
        public int TransmissionId { get; set; } 
        public int FuelTypeId { get; set; } 
        

        [ForeignKey("Model")] 
        public int ModelId { get; set; } 
        public virtual Model Model { get; set; } 


        [ForeignKey("CarCategory")]
        public int CategoryId { get; set; }
        public virtual CarCategory Category { get; set; } 

        public ICollection<Rental>Rentals { get; set; }


        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public virtual Transmission Transmission { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
       


    }
}
