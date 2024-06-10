using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Abouts;
using MVCproject_Elearning.ViewModels.Students;

namespace MVCproject_Elearning.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDBContext _context;
        public StudentService(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Student student)
        {
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string fullname)
        {
            return await _context.Students.AnyAsync(m => m.FullName.Trim() == fullname.Trim());
        }

        public async Task<IEnumerable<StudentVM>> GetAllAsync(int? take = null)
        {
            IEnumerable<Student> student;

            if (take is null)
            {
                student = await _context.Students.ToListAsync();
            }
            else
            {
                student = await _context.Students.Take((int)take).ToListAsync();
            }
            return student.Select(m => new StudentVM { Id = m.Id, FullName = m.FullName, Profession = m.Profession, Image = m.Image, Biography = m.Biography });
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

		public async Task<Student> GetByIdWithAllDatasAsync(int id)
		{
            return await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
		}
	}
}
