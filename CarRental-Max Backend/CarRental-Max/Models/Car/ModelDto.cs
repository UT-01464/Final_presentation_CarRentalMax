using CAR_RENTAL_MS_III.Entities;

namespace CAR_RENTAL_MS_III.Models.Car
{
    public class ModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Make { get; set; }
    }
}
