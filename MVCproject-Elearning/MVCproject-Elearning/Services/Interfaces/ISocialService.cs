using Microsoft.AspNetCore.Mvc.Rendering;
using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface ISocialService
    {
        Task<SelectList> GetAllSelectedAsync();
        Task<IEnumerable<Social>>GetAllAsync();
        Task<Social> GetByIdAsync(int id);
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Social social);
        Task DeleteAsync(Social social);
        Task EditAsync();
    }
}
