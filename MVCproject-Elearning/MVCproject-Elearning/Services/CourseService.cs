using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Courses;

namespace MVCproject_Elearning.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDBContext _context;
        public CourseService(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Course course)
        {
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Courses.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.Include(m => m.CoursesImages).Include(m => m.Category).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Courses.Where(m => !m.SoftDeleted).Include(m => m.CoursesImages)
                .Include(m=>m.Instructor).Include(m => m.Category)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }



        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Course> GetByIdWithCoursesImagesAsync(int id)
        {
            return await _context.Courses.Include(m => m.CoursesImages).Include(m=>m.Instructor).Include(m=>m.CourseStudents).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Courses.CountAsync();
        }

        public async Task<CourseImage> GetCourseImageByIdAsync(int id)
        {
            return await _context.CourseImages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public IEnumerable<CourseVM> GetMappedDatas(IEnumerable<Course> course)
        {
            return course.Select(m => new CourseVM()
            {
                Id = m.Id,
                Name = m.Name,
                CategoryName = m.Category.Name,
                InstructorName=m.Instructor.FullName,
                Price = m.Price,
                Duration = m.Duration,
                Rating = m.Rating,
                MainImage = m.CoursesImages.FirstOrDefault(m => m.IsMain).Name
            }); ;
        }

        public async Task ImageDeleteAsync(CourseImage image)
        {
            _context.CourseImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithAllDatasAsync()
        {
            return await _context.Courses.Include(m => m.CoursesImages).ToListAsync();
        }

        public async Task<CourseImage> GetProductImageByIdAsync(int id)
        {
            return await _context.CourseImages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Course> GetByIdWithAllDatasAsync(int id)
        {
            return await _context.Courses.Where(m => m.Id == id)
                .Include(m => m.Category)
                .Include(m => m.CoursesImages)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetPopularAsync()
        {
            return await _context.Courses.Include(m=>m.CoursesImages).Include(m => m.Category).Where(m=>m.Rating==5).Take(3).ToListAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var courses = await _context.Courses.Where(m => !m.SoftDeleted).ToListAsync();
            return new SelectList(courses, "Id", "Name");
        }
    }
}
