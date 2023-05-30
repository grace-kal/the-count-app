using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Count.Models
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CourceTitle { get; set; }
        public double AllCalories { get; set; }
        public bool IsComplete { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Day")]
        public int DayId { get; set; }
        public virtual Day Day { get; set; }

        public virtual IEnumerable<Food>? Foods { get; set; }

    }
}
