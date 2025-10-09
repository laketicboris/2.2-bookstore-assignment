using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using BookstoreApplication.Repositories;


namespace BookstoreApplication.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AuthorDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            PaginatedList<Author> pagedAuthors = await _repository.GetAllPagedAsync(pageNumber, pageSize);
            List<AuthorDto> authorDtos = _mapper.Map<List<AuthorDto>>(pagedAuthors.Items);

            return new PaginatedList<AuthorDto>(authorDtos, pagedAuthors.Count, pagedAuthors.PageIndex, pageSize);
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