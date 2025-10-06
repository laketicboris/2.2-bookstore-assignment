using BookstoreApplication.Models;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _service;

        public PublishersController(PublisherService publisherService)
        {
            _service = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Publisher> publishers = await _service.GetAllAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Publisher? publisher = await _service.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Publisher publisher)
        {
            Publisher createdPublisher = await _service.CreateAsync(publisher);
            return Ok(createdPublisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            Publisher? updatedPublisher = await _service.UpdateAsync(id, publisher);
            if (updatedPublisher == null)
            {
                return NotFound();
            }

            return Ok(updatedPublisher);
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