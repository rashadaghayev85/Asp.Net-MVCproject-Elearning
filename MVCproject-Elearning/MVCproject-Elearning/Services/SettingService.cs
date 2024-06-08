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
        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }
    }
}
