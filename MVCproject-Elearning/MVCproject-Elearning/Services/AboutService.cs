using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Abouts;
using System.Reflection.Metadata;

namespace MVCproject_Elearning.Services
{
    public class AboutService:IAboutService
    {
        private readonly AppDBContext _context;
        public AboutService(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(About about)
        {
            await _context.AddAsync(about);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(About about)
        {
            _context.Remove(about);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string title)
        {
            return await _context.Abouts.AnyAsync(m => m.Title.Trim() == title.Trim());
        }

        public async Task<IEnumerable<AboutVM>> GetAllAsync(int? take = null)
        {
            IEnumerable<About> blogs;
        
            if (take is null)
            {
                blogs = await _context.Abouts.ToListAsync();
            }
            else
            {
                blogs = await _context.Abouts.Take((int)take).ToListAsync();
            }
            return blogs.Select(m => new AboutVM { Id = m.Id, Title = m.Title, Description = m.Description, Image = m.Image, CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy") });
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _context.Abouts.Where(m => m.Id == id).FirstOrDefaultAsync();
        }
    }
}
