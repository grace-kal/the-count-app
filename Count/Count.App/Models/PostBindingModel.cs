using Count.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Count.App.Models
{
    public class PostBindingModel
    {
        public int? Id { get; set; }
        public string? Summary { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
