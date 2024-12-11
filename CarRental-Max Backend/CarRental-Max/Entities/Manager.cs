using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Manager
    {

        [Key]
        public int ManagerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } 

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Rental> Rentals { get; set; }



    }
}
