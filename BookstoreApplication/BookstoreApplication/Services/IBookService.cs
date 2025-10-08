using BookstoreApplication.Models;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<BookDetailsDto> GetByIdAsync(int id);
        Task<Book?> CreateAsync(Book book);
        Task<Book?> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
    }
}