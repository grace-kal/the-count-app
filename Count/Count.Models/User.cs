using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Count.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }

        public bool IsDelete { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }
        public virtual IEnumerable<BmiUser> UserBmis { get; set; }
        public virtual IEnumerable<Day> Days { get; set; }
    }
}
