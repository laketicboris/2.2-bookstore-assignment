using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherRepository _repository;

        public PublishersController(PublisherRepository publisherRepository)
        {
            _repository = publisherRepository;
        }

        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var publishers = await _repository.GetAllAsync();
            return Ok(publishers);
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> Post(Publisher publisher)
        {
            var createdPublisher = await _repository.CreateAsync(publisher);
            return Ok(createdPublisher);
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            var updatedPublisher = await _repository.UpdateAsync(id, publisher);
            if (updatedPublisher == null)
            {
                return NotFound();
            }

            return Ok(updatedPublisher);
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}