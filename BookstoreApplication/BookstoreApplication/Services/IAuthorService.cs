using BookstoreApplication.Models;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IAuthorService
    {
        Task<PaginatedList<AuthorDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<Author?> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author author);
        Task<Author?> UpdateAsync(int id, Author author);
        Task<bool> DeleteAsync(int id);
    }
}
