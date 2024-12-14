namespace CarRental_Max.Models.Rentals
{
    public class RentCarRequest
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int RentalDays { get; set; }
    }
}
