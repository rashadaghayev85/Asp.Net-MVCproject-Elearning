using Microsoft.AspNetCore.Mvc.Rendering;
using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface IIconService
    {
        Task<SelectList> GetAllSelectedAsync();
        Task<IEnumerable<InformationIcon>> GetAllAsync();
        Task CreateAsync(InformationIcon icon);
        Task DeleteAsync(InformationIcon icon);
        Task EditAsync();
        Task<InformationIcon> GetByIdAsync(int id);
    }
}
