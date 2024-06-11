using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Services.Interfaces;
using NuGet.Configuration;

namespace MVCproject_Elearning.Services
{
    public class SettingService:ISettingService
    {
        private readonly AppDBContext _context;
        public SettingService(AppDBContext context)
        {
            _context = context;
        }

        public async Task EditAsync()
        {
           await _context.SaveChangesAsync();   
        }

        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);

           
        }

        public async Task<Dictionary<string, string>> GetByIdAsync(int id)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);

            if (setting == null)
            {
                return null; 
            }

            var dictionary = new Dictionary<string, string>
    {
        { "Id", setting.Id.ToString() },
        { "Name", setting.Key },
        { "Value", setting.Value }
        
    };

            return dictionary;
        }

        public async Task<Dictionary<string, string>> GetByKeyAsync(string key)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(m => m.Key == key);

            if (setting == null)
            {
                return null;
            }

            var dictionary = new Dictionary<string, string>
    {
        { "Id", setting.Id.ToString() },
        { "Name", setting.Key },
        { "Value", setting.Value }

    };

            return dictionary;
        }
    }
}
