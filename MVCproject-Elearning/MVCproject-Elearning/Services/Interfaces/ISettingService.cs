using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAllAsync();
        Task<Dictionary<string, string>> GetByIdAsync(int id);
        Task<Dictionary<string, string>> GetByKeyAsync(string key);
        Task EditAsync();
    }
}
