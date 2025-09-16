using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublisherRepository
    {
        private readonly AppDbContext _context;

        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher?> UpdateAsync(int id, Publisher publisher)
        {
            var existingPublisher = await _context.Publishers.FindAsync(id);
            if (existingPublisher == null)
                return null;

            existingPublisher.Name = publisher.Name;
            existingPublisher.Address = publisher.Address;
            existingPublisher.Website = publisher.Website;

            await _context.SaveChangesAsync();
            return existingPublisher;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                return false;

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}