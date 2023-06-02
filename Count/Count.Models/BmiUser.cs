using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Models
{
    public class BmiUser
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Bmi Bmi { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Height { get; set; }
        public double GoalWeight { get; set; }
        public double CalculatedBmi { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
