using MVCproject_Elearning.Models;
using MVCproject_Elearning.ViewModels.Abouts;
using MVCproject_Elearning.ViewModels.Students;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentVM>> GetAllAsync(int? take = null);
        Task<Student> GetByIdAsync(int id);
        Task<bool> ExistAsync(string fullname);
        Task CreateAsync(Student student);
        Task DeleteAsync(Student student);
        Task EditAsync();
    }
}
