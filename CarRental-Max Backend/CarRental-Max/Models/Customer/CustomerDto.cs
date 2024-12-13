namespace CarRental_Max.Models.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string Nic { get; set; } // National Identity Card
        public AddressDto? Address { get; set; } // Address details
    }
}
