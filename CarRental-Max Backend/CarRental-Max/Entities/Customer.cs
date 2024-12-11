using CarRental_Max.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Customer
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string Nic { get; set; } // National Identity Card
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Address Address { get; set; }
        

    }

    public enum Role
    {
        Admin = 1,
        Customer = 2
    }
}
