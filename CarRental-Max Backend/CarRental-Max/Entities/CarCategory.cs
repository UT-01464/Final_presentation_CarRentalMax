using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class CarCategory
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; } 
        public virtual ICollection<Model> CarModels { get; set; } 
        public virtual ICollection<Car> Cars { get; set; } 
    }
}
