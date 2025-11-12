using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AwardRepository : IAwardRepository
    {
        private readonly AppDbContext _context;

        public AwardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Award>> GetAllAsync()
        {
            return await _context.Awards.ToListAsync();
        }

        public async Task<Award?> GetByIdAsync(int id)
        {
            return await _context.Awards.FindAsync(id);
        }

        public async Task<Award> CreateAsync(Award award)
        {
            _context.Awards.Add(award);
            return award;
        }

        public async Task<Award?> UpdateAsync(int id, Award award)
        {
            var existingAward = await _context.Awards.FindAsync(id);
            if (existingAward == null)
                return null;

            existingAward.Name = award.Name;
            existingAward.Description = award.Description;
            existingAward.StartYear = award.StartYear;

            return existingAward;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null)
                return false;

            _context.Awards.Remove(award);
            return true;
        }
    }
}