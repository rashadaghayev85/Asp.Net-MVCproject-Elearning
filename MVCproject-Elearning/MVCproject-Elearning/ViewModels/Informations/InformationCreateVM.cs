using System.ComponentModel.DataAnnotations;

namespace MVCproject_Elearning.ViewModels.Informations
{
    public class InformationCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int  IconId { get; set; }
    }
}
