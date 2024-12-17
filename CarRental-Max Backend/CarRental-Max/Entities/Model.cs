using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Entities
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public int CategoryId { get; set; }
        public virtual CarCategory Category { get; set; }
        public virtual ICollection<Car> Cars { get; set; }

    }
}
