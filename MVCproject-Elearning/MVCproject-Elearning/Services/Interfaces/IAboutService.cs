using MVCproject_Elearning.Models;
using MVCproject_Elearning.ViewModels.Abouts;
using System.Reflection.Metadata;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface IAboutService
    {
        Task<IEnumerable<AboutVM>> GetAllAsync(int? take = null);
        Task<About> GetByIdAsync(int id);
        Task<bool> ExistAsync(string title);
        Task CreateAsync(About about);
        Task DeleteAsync(About about);
        Task EditAsync();
    }
}
