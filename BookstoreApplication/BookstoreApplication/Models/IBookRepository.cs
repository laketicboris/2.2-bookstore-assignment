using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories
{
    public interface IBookRepository
    {
        Task<PagedResult<Book>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<PagedResult<Book>> GetAllSortedAsync(BookSortType sortType, int page = 1, int pageSize = 10);
        Task<PagedResult<Book>> GetAllFilteredAndSortedAsync(BookFilterDto filter, BookSortType sortType, int page = 1, int pageSize = 10);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> CreateAsync(Book book);
        Task<Book?> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
        List<BookSortTypeOption> GetSortTypes();
    }
}
