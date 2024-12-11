using CAR_RENTAL_MS_III.Entities;
using CarRental_Max.Models.Car;

namespace CarRental_Max.Models.Rentals
{
    public class RentalDetailsDto
    {
        
        
        
       
       

        public int Id { get; set; }
        public CarDto Car { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNIC { get; set; }
        public DateTime RentalDate { get; set; }
        public RentalStatus Status { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsOverdue { get; set; }
    }
}
