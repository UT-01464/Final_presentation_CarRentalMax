namespace CarRental_Max.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsResponded { get; set; }
        public string? AdminResponse { get; set; }
    }
}
