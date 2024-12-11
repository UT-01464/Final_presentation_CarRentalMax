namespace CarRental_Max.Models.Rentals
{
    public class OverdueCarDto
    {
        public int RentalId { get; set; }
        public string CustomerName { get; set; }
        public int ModelId { get; set; }
        public DateTime DueDate { get; set; }
        public decimal OverdueFees { get; set; }
    }
}
