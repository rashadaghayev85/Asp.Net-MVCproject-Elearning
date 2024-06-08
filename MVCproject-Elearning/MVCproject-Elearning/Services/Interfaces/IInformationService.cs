using Microsoft.VisualBasic;
using MVCproject_Elearning.Models;
using Information = MVCproject_Elearning.Models.Information;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface IInformationService
    {
        Task<IEnumerable<Information>> GetAllAsync();
        Task CreateAsync(Information information);
        Task DeleteAsync(Information information);
        Task EditAsync();
        Task<Information> GetByIdAsync(int id);
		Task<Information> GetByIdWithIconAsync(int id);
	}
}
