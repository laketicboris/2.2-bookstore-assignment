using BookstoreApplication.Models;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly IAwardService _service;

        public AwardsController(IAwardService awardService)
        {
            _service = awardService;
        }

        // GET: api/awards
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Award> awards = await _service.GetAllAsync();
            return Ok(awards);
        }

        // GET api/awards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Award? award = await _service.GetByIdAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return Ok(award);
        }

        // POST api/awards
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Award award)
        {
            Award createdAward = await _service.CreateAsync(award);
            return Ok(createdAward);
        }

        // PUT api/awards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Award award)
        {
            if (id != award.Id)
            {
                return BadRequest();
            }

            Award? updatedAward = await _service.UpdateAsync(id, award);
            if (updatedAward == null)
            {
                return NotFound();
            }

            return Ok(updatedAward);
        }

        // DELETE api/awards/5
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