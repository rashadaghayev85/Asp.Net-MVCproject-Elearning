namespace MVCproject_Elearning.ViewModels.Students
{
    public class StudentCreateVM
    {
        public string FullName { get; set; }
        public string? Biography { get; set; }
        public string Profession { get; set; }
        public string Course { get; set; }
        public IFormFile Image { get; set; }
    }
}
