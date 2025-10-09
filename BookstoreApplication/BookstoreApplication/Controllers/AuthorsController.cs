using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService authorService)
        {
            _service = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            PaginatedList<AuthorDto> authors = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Author? author = await _service.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author)
        {
            Author createdAuthor = await _service.CreateAsync(author);
            return Ok(createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            Author? updatedAuthor = await _service.UpdateAsync(id, author);
            if (updatedAuthor == null)
            {
                return NotFound();
            }

            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _service.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}