using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class CarCategory
    {

        [Key]
        public int Id { get; set; } // Unique identifier for the category

        public string Name { get; set; } // Name of the category
        public virtual ICollection<Model> CarModels { get; set; } // Navigation property
        public virtual ICollection<Car> Cars { get; set; } // Navigation property
    }
}
