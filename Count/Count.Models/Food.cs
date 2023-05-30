using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace Count.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("User")]
        public string? CreatedById { get; set; }
        public virtual User? CreatedBy { get; set; }

        public virtual IEnumerable<Meal>? Meals { get;set; }
    }
}
