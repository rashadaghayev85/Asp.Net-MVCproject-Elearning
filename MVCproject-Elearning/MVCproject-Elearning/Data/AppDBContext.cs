using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
		public DbSet<Information> Informations { get; set; }
		public DbSet<InformationIcon> InformationIcons { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorSocial> InstructorSocials { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Student> Students { get; set; }


    }
}
