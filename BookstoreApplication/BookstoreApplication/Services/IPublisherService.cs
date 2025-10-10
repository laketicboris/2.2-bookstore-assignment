using BookstoreApplication.Models;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IPublisherService
    {
        Task<List<Publisher>> GetAllAsync();
        Task<List<Publisher>> GetAllSortedAsync(PublisherSortType sortType);
        Task<Publisher> GetByIdAsync(int id);
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
        Task DeleteAsync(int id);
        List<SortTypeOption> GetSortTypes();
    }
}