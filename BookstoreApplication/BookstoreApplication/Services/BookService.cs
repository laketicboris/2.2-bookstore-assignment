using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            List<Book> books = await _repository.GetAllAsync();
            List<BookDto> bookDtos = _mapper.Map<List<BookDto>>(books);
            return bookDtos;
        }

        public async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            Book? book = await _repository.GetByIdAsync(id);
            if (book == null)
                return null;

            BookDetailsDto bookDetailsDto = _mapper.Map<BookDetailsDto>(book);
            return bookDetailsDto;
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