

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Count.Models
{
    public class MealFood
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Meal")]
        public int MealId { get; set; }
        public virtual Meal? Meal { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public virtual Food? Food { get; set; }

        [Required]
        public double Quantity { get; set; }
        public double Calories { get; set; }
    }
}
