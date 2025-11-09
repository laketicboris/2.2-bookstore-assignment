using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Exceptions;
using System.Text.Json;

namespace BookstoreApplication.Services
{
    public class IssueService : IIssueService
    {
        private readonly IComicVineConnection _comicVineConnection;
        private readonly IIssueRepository _issueRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<IssueService> _logger;

        public IssueService(
            IComicVineConnection comicVineConnection,
            IIssueRepository issueRepository,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<IssueService> logger)
        {
            _comicVineConnection = comicVineConnection;
            _issueRepository = issueRepository;
            _config = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<IssueDto>> SearchIssuesByVolume(int volumeId)
        {
            _logger.LogInformation("Searching issues for volume ID: {VolumeId}", volumeId);

            var url = $"{_config["ComicVineBaseUrl"]}/issues" +
              $"?api_key={_config["ComicVineAPIKey"]}" +
              $"&format=json" +
              $"&filter=volume:{volumeId}" +
              $"&field_list=id,name,issue_number,cover_date,image,description,page_count,volume";

            var json = await _comicVineConnection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var issues = JsonSerializer.Deserialize<List<IssueDto>>(json, options) ?? new List<IssueDto>();

            _logger.LogInformation("Found {Count} issues for volume ID: {VolumeId}", issues.Count, volumeId);

            return issues;
        }

        public async Task<int> CreateIssueAsync(SaveIssueDto saveIssueDto)
        {
            _logger.LogInformation("Creating issue with external API ID: {ExternalId}", saveIssueDto.ExternalApiId);

            var issueDetails = await GetIssueDetailsFromApi(saveIssueDto.ExternalApiId);

            var issue = new Issue
            {
                Name = issueDetails.Name ?? "Unknown Issue",
                ReleaseDate = issueDetails.CoverDate.HasValue
                    ? DateTime.SpecifyKind(issueDetails.CoverDate.Value, DateTimeKind.Utc)
                    : DateTime.UtcNow,
                IssueNumber = issueDetails.IssueNumber ?? 0,
                ImagePath = issueDetails.ImageUrl,
                Description = issueDetails.Description,
                ExternalApiId = saveIssueDto.ExternalApiId,
                PageCount = issueDetails.PageCount ?? 0,
                Price = saveIssueDto.Price,
                AvailableCopies = saveIssueDto.AvailableCopies,
                CreatedAt = DateTime.UtcNow,
                VolumeApiDetailUrl = issueDetails.Volume?.ApiDetailUrl,
                VolumeId = issueDetails.Volume?.Id,
                VolumeName = issueDetails.Volume?.Name
            };

            var createdIssue = await _issueRepository.CreateAsync(issue);

            _logger.LogInformation("Issue created successfully with ID: {IssueId}", createdIssue.Id);

            return createdIssue.Id;
        }

        public async Task<IssueDto> GetIssueByIdAsync(int id)
        {
            var issue = await _issueRepository.GetByIdAsync(id);
            if (issue == null)
                throw new NotFoundException($"Issue with ID {id} not found");

            return _mapper.Map<IssueDto>(issue);
        }

        private async Task<IssueDto> GetIssueDetailsFromApi(int issueId)
        {
            var url = $"{_config["ComicVineBaseUrl"]}/issue/4000-{issueId}" +
              $"?api_key={_config["ComicVineAPIKey"]}" +
              $"&format=json" +
              $"&field_list=id,name,issue_number,cover_date,image,description,page_count,volume";

            var json = await _comicVineConnection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var issue = JsonSerializer.Deserialize<IssueDto>(json, options);

            if (issue == null)
                throw new NotFoundException($"Issue with ID {issueId} not found on Comic Vine API");

            return issue;
        }
    }
}