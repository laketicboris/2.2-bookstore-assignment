using BookstoreApplication.Models;
using BookstoreApplication.DTOs;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _service;

        public PublishersController(IPublisherService publisherService)
        {
            _service = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PublisherSortType sortType = PublisherSortType.NameAscending)
        {
            List<Publisher> publishers = await _service.GetAllSortedAsync(sortType);
            return Ok(publishers);
        }

        [HttpGet("sortTypes")]
        public IActionResult GetSortTypes()
        {
            List<SortTypeOption> sortTypes = _service.GetSortTypes();
            return Ok(sortTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Publisher publisher = await _service.GetByIdAsync(id);
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
            Publisher updatedPublisher = await _service.UpdateAsync(id, publisher);
            return Ok(updatedPublisher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}