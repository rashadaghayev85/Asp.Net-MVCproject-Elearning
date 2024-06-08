using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.ViewModels.Courses
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
        public int? InstructorId { get; set; }
        public string? MainImage { get; set; }
        public int ? Rating { get; set; }
        public int? Duration { get; set; }
      
    }
}
