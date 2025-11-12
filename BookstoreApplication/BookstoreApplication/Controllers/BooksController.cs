using BookstoreApplication.Models;
using BookstoreApplication.DTOs;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService bookService)
        {
            _service = bookService;
        }

        // GET /api/books?sortType=TitleAscending&page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BookSortType sortType = BookSortType.TitleAscending, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var paged = await _service.GetAllSortedAsync(sortType, page, pageSize);
            return Ok(paged);
        }

        [HttpPost("filterAndSort")]
        public async Task<IActionResult> GetFilteredAndSorted(
            [FromBody] BookFilterDto filter,
            [FromQuery] BookSortType sortType = BookSortType.TitleAscending,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var paged = await _service.GetAllFilteredAndSortedAsync(filter, sortType, page, pageSize);
            return Ok(paged);
        }

        [HttpGet("sortTypes")]
        public IActionResult GetSortTypes()
        {
            List<BookSortTypeOption> sortTypes = _service.GetSortTypes();
            return Ok(sortTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            BookDetailsDto book = await _service.GetByIdAsync(id);
            return Ok(book);
        }

        [Authorize(Roles = "Librarian,Editor")]
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            Book? createdBook = await _service.CreateAsync(book);
            return Ok(createdBook);
        }

        [Authorize(Roles = "Editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            Book? updatedBook = await _service.UpdateAsync(id, book);
            return Ok(updatedBook);
        }

        [Authorize(Roles = "Editor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}