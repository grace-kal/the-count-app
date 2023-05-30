using Count.Models;
using System.ComponentModel.DataAnnotations;

namespace Count.App.Models
{
    public class BmiUserBindingModel
    {
        public int Id { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public string UserId { get; set; }
        public double CalculatedBmi { get; set; }
        public DateTime Date { get; set; }
        public Bmi Bmi { get; set; }
        public bool IsDeleted { get; set; }


    }
}
