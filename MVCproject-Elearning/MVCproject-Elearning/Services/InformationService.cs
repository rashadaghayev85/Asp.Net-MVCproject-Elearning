using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;

namespace MVCproject_Elearning.Services
{
    public class InformationService : IInformationService
    {
        private readonly AppDBContext _context;
        public InformationService(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Information information)
        {
            await _context.Informations.AddAsync(information);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Information information)
        {
            _context.Informations.Remove(information);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Information>> GetAllAsync()
        {
            return await _context.Informations.Include(m=>m.InformationIcon).ToListAsync();
        }

        public async Task<Information> GetByIdAsync(int id)
        {
            return _context.Informations.FirstOrDefault(m => m.Id == id);
        }

		public async Task<Information> GetByIdWithIconAsync(int id)
		{
			return _context.Informations.Include(m=>m.InformationIcon).FirstOrDefault(m => m.Id == id);
		}
	}
}
