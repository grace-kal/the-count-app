using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Models
{
    public class Day
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int AllDayCalories { get; set; }//acumulutive from each meal in Meals
        public bool IsDeleted { get; set; }
        public bool IsComplete { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual IEnumerable<Meal>? Meals { get; set; }

    }
}
