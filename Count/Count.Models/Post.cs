using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime PostedOn { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("User")]
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}
