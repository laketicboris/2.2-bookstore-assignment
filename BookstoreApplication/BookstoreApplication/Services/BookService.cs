using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Exceptions;
using Microsoft.Extensions.Logging;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository repository, IMapper mapper, ILogger<BookService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            _logger.LogInformation("BookService.GetAllAsync called");

            List<Book> books = await _repository.GetAllAsync();
            List<BookDto> bookDtos = _mapper.Map<List<BookDto>>(books);

            _logger.LogInformation("BookService.GetAllAsync returned {Count} books", bookDtos.Count);

            return bookDtos;
        }

        public async Task<List<BookDto>> GetAllSortedAsync(BookSortType sortType)
        {
            _logger.LogInformation("BookService.GetAllSortedAsync called with sortType: {SortType}", sortType);

            List<Book> books = await _repository.GetAllSortedAsync(sortType);
            List<BookDto> bookDtos = _mapper.Map<List<BookDto>>(books);

            _logger.LogInformation("BookService.GetAllSortedAsync returned {Count} books", bookDtos.Count);

            return bookDtos;
        }

        public List<BookSortTypeOption> GetSortTypes()
        {
            return _repository.GetSortTypes();
        }

        public async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            _logger.LogInformation("BookService.GetByIdAsync called with ID: {BookId}", id);

            Book? book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Book {BookId} not found, throwing NotFoundException.", id);
                throw new NotFoundException(id);
            }

            BookDetailsDto bookDetailsDto = _mapper.Map<BookDetailsDto>(book);
            _logger.LogInformation("Book {BookId} found and mapped successfully.", id);
            return bookDetailsDto;
        }

        public async Task<Book?> CreateAsync(Book book)
        {
            _logger.LogInformation("BookService.CreateAsync called for title: {Title}", book.Title);

            Book? createdBook = await _repository.CreateAsync(book);
            if (createdBook == null)
            {
                _logger.LogError("Creation failed for book {Title}. Author or Publisher not found.", book.Title);
                throw new BadRequestException("Author or Publisher not found");
            }
            _logger.LogInformation("Book created successfully with ID: {BookId}", createdBook.Id);
            return createdBook;
        }

        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            _logger.LogInformation("BookService.UpdateAsync called for ID: {BookId}", id);
            if (id != book.Id)
            {
                _logger.LogWarning("Update failed: ID in URL ({UrlId}) does not match ID in body ({BodyId}).", id, book.Id);
                throw new BadRequestException("ID in URL does not match ID in body");
            }

            Book? updatedBook = await _repository.UpdateAsync(id, book);
            if (updatedBook == null)
            {
                _logger.LogWarning("Update failed: Book {BookId} not found, throwing NotFoundException.", id);
                throw new NotFoundException(id);
            }
            _logger.LogInformation("Book {BookId} updated successfully.", id);
            return updatedBook;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("BookService.DeleteAsync called for ID: {BookId}", id);

            bool deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Delete failed: Book {BookId} not found, throwing NotFoundException.", id);
                throw new NotFoundException(id);
            }
            _logger.LogInformation("Book {BookId} deleted successfully.", id);
            return deleted;
        }
    }
}