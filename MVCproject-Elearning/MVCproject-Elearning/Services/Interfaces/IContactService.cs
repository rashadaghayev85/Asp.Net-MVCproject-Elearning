using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task CreateAsync(Contact contact);
        Task<Contact> GetByIdAsync(int id);
        Task DeleteAsync(Contact contact);
    }
}
