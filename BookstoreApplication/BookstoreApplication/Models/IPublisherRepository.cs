using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAllAsync();
        Task<List<Publisher>> GetAllSortedAsync(PublisherSortType sortType);
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher?> UpdateAsync(int id, Publisher publisher);
        Task<bool> DeleteAsync(int id);
        List<SortTypeOption> GetSortTypes();
    }
}