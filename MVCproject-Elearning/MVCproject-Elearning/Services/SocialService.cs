﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;

namespace MVCproject_Elearning.Services
{
    public class SocialService : ISocialService
    {
        private readonly AppDBContext _context;
        public SocialService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Social>> GetAllAsync()
        {
            return await _context.Socials.ToListAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var socials = await _context.Socials.Where(m => !m.SoftDeleted).ToListAsync();
            return new SelectList(socials, "Id", "Name");
        }
        public async Task CreateAsync(Social social)
        {
            await _context.Socials.AddAsync(social);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Social social)
        {
            _context.Socials.Remove(social);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Socials.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<Social> GetByIdAsync(int id)
        {
           return await _context.Socials.FirstOrDefaultAsync(m => m.Id == id);    
        }
    }
}
