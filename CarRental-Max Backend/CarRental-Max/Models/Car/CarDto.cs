namespace CarRental_Max.Models.Car
{
    public class CarDto
    {

        public int Id { get; set; }
       
        public int ModelId { get; set; }
        public int Year { get; set; }
        public string RegistrationNumber { get; set; }
        public int CategoryId { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfSeats { get; set; }


        public int TransmissionId { get; set; }
        public int FuelTypeId { get; set; }
        public List<int> FeatureIds { get; set; } // List of feature IDs
    }
}
