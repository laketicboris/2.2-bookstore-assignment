using BookstoreApplication.DTOs;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        // GET /api/issues/search?volumeId=123&page=1&pageSize=10
        [HttpGet("search")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> SearchIssuesByVolume([FromQuery] int volumeId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (volumeId <= 0)
            {
                return BadRequest("Volume ID must be a positive number");
            }

            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var pagedResult = await _issueService.SearchIssuesByVolume(volumeId, page, pageSize);
            return Ok(pagedResult);
        }

        // POST /api/issues
        [HttpPost]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> CreateIssue([FromBody] SaveIssueDto saveIssueDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var issueId = await _issueService.CreateIssueAsync(saveIssueDto);
            return CreatedAtAction(nameof(GetIssue), new { id = issueId }, new { id = issueId });
        }

        // GET /api/issues/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> GetIssue(int id)
        {
            var issue = await _issueService.GetIssueByIdAsync(id);
            return Ok(issue);
        }
    }
}