using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;

namespace MVCproject_Elearning.Services
{
    public class IconService : IIconService
    {
        private readonly AppDBContext _context;
        public IconService(AppDBContext context)
        {
            _context = context;
        }
        public async Task<SelectList> GetAllSelectedAsync()
        {
            var icons = await _context.InformationIcons.Where(m => !m.SoftDeleted).ToListAsync();
            return new SelectList(icons, "Id", "Icon");
        }
        public async Task CreateAsync(InformationIcon icon)
        {
            await _context.InformationIcons.AddAsync(icon);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(InformationIcon icon)
        {
            _context.InformationIcons.Remove(icon);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InformationIcon>> GetAllAsync()
        {
            return await _context.InformationIcons.ToListAsync();
        }

        public async Task<InformationIcon> GetByIdAsync(int id)
        {
            return _context.InformationIcons.FirstOrDefault(m => m.Id == id);
        }
    }
}
