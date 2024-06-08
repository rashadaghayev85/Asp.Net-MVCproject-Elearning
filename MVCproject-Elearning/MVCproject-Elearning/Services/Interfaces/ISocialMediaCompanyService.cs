using MVCproject_Elearning.Models;

namespace MVCproject_Elearning.Services.Interfaces
{
    public interface ISocialMediaCompanyService
    {
        Task<IEnumerable<SocialMediaCompany>> GetAllAsync();
    }
}
