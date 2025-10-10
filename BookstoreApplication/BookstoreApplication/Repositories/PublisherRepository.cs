using BookstoreApplication.Models;
using BookstoreApplication.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublisherRepository : IPublisherRepository
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

        public async Task<List<Publisher>> GetAllSortedAsync(PublisherSortType sortType)
        {
            IQueryable<Publisher> query = _context.Publishers;

            query = sortType switch
            {
                PublisherSortType.NameAscending => query.OrderBy(p => p.Name),
                PublisherSortType.NameDescending => query.OrderByDescending(p => p.Name),
                PublisherSortType.AddressAscending => query.OrderBy(p => p.Address),
                PublisherSortType.AddressDescending => query.OrderByDescending(p => p.Address),
                _ => query.OrderBy(p => p.Name)
            };

            return await query.ToListAsync();
        }

        public List<SortTypeOption> GetSortTypes()
        {
            List<SortTypeOption> options = new List<SortTypeOption>();
            var enumValues = Enum.GetValues(typeof(PublisherSortType));

            foreach (PublisherSortType sortType in enumValues)
            {
                options.Add(new SortTypeOption(sortType));
            }

            return options;
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