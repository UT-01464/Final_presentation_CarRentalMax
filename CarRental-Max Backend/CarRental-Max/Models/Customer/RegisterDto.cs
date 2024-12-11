using CAR_RENTAL_MS_III.Entities;

namespace CarRental_Max.Models.Customer
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string Nic { get; set; } // National Identity Card
        public string Password { get; set; }
       

        
    }
}
