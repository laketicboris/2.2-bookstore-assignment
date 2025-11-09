using BookstoreApplication.DTOs;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumesController : ControllerBase
    {
        private readonly IVolumeService _volumeService;

        public VolumesController(IVolumeService volumeService)
        {
            _volumeService = volumeService;
        }

        // GET /api/volumes/search?query=Batman
        [HttpGet("search")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> SearchVolumes([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty");
            }

            var volumes = await _volumeService.SearchVolumesByName(query);
            return Ok(volumes);
        }
    }
}