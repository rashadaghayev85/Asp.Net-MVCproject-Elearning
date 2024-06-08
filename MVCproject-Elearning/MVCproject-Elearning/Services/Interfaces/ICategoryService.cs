using Microsoft.AspNetCore.Mvc.Rendering;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.ViewModels.Categories;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllWithAllDatasAsync();

        Task<IEnumerable<Category>> GetAllPaginateAsync(int page, int take);
        IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> category);
        Task<int> GetCountAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task DeleteAsync(Category category);
        Task<SelectList> GetAllSelectedAsync();
        Task<Category> GetByIdWithCoursesAsync(int id);
        Task<bool> ExistExceptByIdAsync(int id, string name);
        Task EditAsync();
    }
}
