using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task CreateAsync(Slider slider);
        Task DeleteAsync(Slider slider);
        Task EditAsync();
        Task<Slider> GetByIdAsync(int id);
    }
}
