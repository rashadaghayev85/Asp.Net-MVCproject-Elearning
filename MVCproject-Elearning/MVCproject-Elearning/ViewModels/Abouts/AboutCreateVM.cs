using System.ComponentModel.DataAnnotations;

namespace MVCproject_Elearning.ViewModels.Abouts
{
    public class AboutCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(20)]
        public string Title { get; set; }

        [Required(ErrorMessage = "This input can't be empty")]
        public string Description { get; set; }
    }
}
