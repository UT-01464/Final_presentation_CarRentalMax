using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Rental
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public DateTime RentalDate { get; set; }
        public RentalStatus Status { get; set; } 
        public DateTime? ReturnDate { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal OverdueFees { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Car Car { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

    }


    public enum RentalStatus
    {
        Pending,
        Accepted,
        Rejected,
        Rented,
        Returned
    }
}



