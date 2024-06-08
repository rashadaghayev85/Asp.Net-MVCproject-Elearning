using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;

namespace MVCproject_Elearning.Services
{
    public class SliderService:ISliderService
    {
        private readonly AppDBContext _context;
        public SliderService(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Slider product)
        {
            await _context.Sliders.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Slider product)
        {
            _context.Sliders.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return _context.Sliders.FirstOrDefault(m => m.Id == id);
        }
    }
}
