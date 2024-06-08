using System.ComponentModel.DataAnnotations;

namespace MVCproject_Elearning.Models
{
    public class Slider:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
