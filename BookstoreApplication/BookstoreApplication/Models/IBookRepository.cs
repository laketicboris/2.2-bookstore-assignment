using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Book>> GetAllSortedAsync(BookSortType sortType);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> CreateAsync(Book book);
        Task<Book?> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
        List<BookSortTypeOption> GetSortTypes();
    }
}