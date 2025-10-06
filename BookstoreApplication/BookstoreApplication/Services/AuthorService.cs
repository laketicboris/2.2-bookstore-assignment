using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _repository;

        public AuthorService(AuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            List<Author> authors = await _repository.GetAllAsync();
            return authors;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            Author? author = await _repository.GetByIdAsync(id);
            return author;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            Author createdAuthor = await _repository.CreateAsync(author);
            return createdAuthor;
        }

        public async Task<Author?> UpdateAsync(int id, Author author)
        {
            Author? updatedAuthor = await _repository.UpdateAsync(id, author);
            return updatedAuthor;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = await _repository.DeleteAsync(id);
            return deleted;
        }
    }
}