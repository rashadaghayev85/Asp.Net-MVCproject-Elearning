using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.ViewModels.Instructors
{
    public class InstructorDetailVM
    {
        public string? FullName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public List<InstructorSocial> InstructorSocials { get; set; }
    }
}
