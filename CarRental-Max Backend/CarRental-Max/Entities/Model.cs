using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Model
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the model
        public string Name { get; set; } // Name of the model
        public string Make { get; set; } // Brand of the model
        public int CategoryId { get; set; } // Foreign key to Category
        public virtual CarCategory Category { get; set; } // Navigation property
        public virtual ICollection<Car> Cars { get; set; } // Navigation property
    }
}
