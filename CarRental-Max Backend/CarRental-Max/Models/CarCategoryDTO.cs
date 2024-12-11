using System.ComponentModel.DataAnnotations;

namespace CAR_RENTAL_MS_III.Models
{
    public class CarCategoryDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}
