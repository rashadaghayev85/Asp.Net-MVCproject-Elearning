using MVCproject_Elearning.Models;
using MVCproject_Elearning.ViewModels.Abouts;
using MVCproject_Elearning.ViewModels.Informations;
using MVCproject_Elearning.ViewModels.Students;
using System.Composition;

namespace MVCproject_Elearning.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Information> Informations { get; set; }

        public IEnumerable<AboutVM> Abouts { get; set; }
        public Category CategoryFirst { get; set; }
        public Category CategoryLast { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<StudentVM> Students { get; set; }

    }
}
