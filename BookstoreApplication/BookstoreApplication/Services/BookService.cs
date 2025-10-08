using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Exceptions;

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
            {
                throw new NotFoundException(id);
            }

            BookDetailsDto bookDetailsDto = _mapper.Map<BookDetailsDto>(book);
            return bookDetailsDto;
        }

        public async Task<Book?> CreateAsync(Book book)
        {
            Book? createdBook = await _repository.CreateAsync(book);
            if (createdBook == null)
            {
                throw new BadRequestException("Author or Publisher not found");
            }
            return createdBook;
        }

        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            if (id != book.Id)
            {
                throw new BadRequestException("ID in URL does not match ID in body");
            }

            Book? updatedBook = await _repository.UpdateAsync(id, book);
            if (updatedBook == null)
            {
                throw new NotFoundException(id);
            }
            return updatedBook;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException(id);
            }
            return deleted;
        }
    }
}