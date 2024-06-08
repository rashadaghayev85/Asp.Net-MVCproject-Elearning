using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;

namespace MVCproject_Elearning.Services
{
    public class SocialMediaCompanyService : ISocialMediaCompanyService
    {
        private readonly AppDBContext _context;
        public SocialMediaCompanyService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SocialMediaCompany>> GetAllAsync()
        {
            return await _context.SocialMediasCompany.ToListAsync();
        }
    }
}
