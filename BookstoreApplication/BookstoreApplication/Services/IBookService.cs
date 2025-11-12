using BookstoreApplication.Models;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IBookService
    {
        Task<PagedResult<BookDto>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<PagedResult<BookDto>> GetAllSortedAsync(BookSortType sortType, int page = 1, int pageSize = 10);
        Task<PagedResult<BookDto>> GetAllFilteredAndSortedAsync(BookFilterDto filter, BookSortType sortType, int page = 1, int pageSize = 10);
        Task<BookDetailsDto> GetByIdAsync(int id);
        Task<Book?> CreateAsync(Book book);
        Task<Book?> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
        List<BookSortTypeOption> GetSortTypes();
    }
}