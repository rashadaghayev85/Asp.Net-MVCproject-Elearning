using MVCproject_Elearning.Models;
using MVCproject_Elearning.ViewModels.Instructors;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task CreateAsync(Instructor instructor);
        Task DeleteAsync(Instructor instructor);
        Task EditAsync();
        Task<bool>ExistEmailAsync(string email);
        Task<Instructor> GetByIdAsync(int id);
        Task<Instructor> GetByIdWithSocialAsync(int id);
        Task<bool>ExistExceptByIdAsync(int id, string email); 
    }
}
