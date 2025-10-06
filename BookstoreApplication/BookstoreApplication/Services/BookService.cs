using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            List<Book> books = await _repository.GetAllAsync();
            return books;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            Book? book = await _repository.GetByIdAsync(id);
            return book;
        }

        public async Task<Book?> CreateAsync(Book book)
        {
            Book? createdBook = await _repository.CreateAsync(book);
            return createdBook;
        }

        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            Book? updatedBook = await _repository.UpdateAsync(id, book);
            return updatedBook;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = await _repository.DeleteAsync(id);
            return deleted;
        }
    }
}