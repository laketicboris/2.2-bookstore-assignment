using BookstoreApplication.DTOs;
using System.Text.Json;

namespace BookstoreApplication.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly IComicVineConnection _comicVineConnection;
        private readonly IConfiguration _config;
        private readonly ILogger<VolumeService> _logger;

        public VolumeService(IComicVineConnection comicVineConnection, IConfiguration configuration, ILogger<VolumeService> logger)
        {
            _comicVineConnection = comicVineConnection;
            _config = configuration;
            _logger = logger;
        }

        public async Task<PagedResult<VolumeDto>> SearchVolumesByName(
            string searchQuery,
            int page = 1,
            int pageSize = 10)
        {
            _logger.LogInformation("Searching volumes for query: {Query}, page: {Page}", searchQuery, page);

            int offset = (page - 1) * pageSize;

            var url = $"{_config["ComicVineBaseUrl"]}/volumes" +
                $"?api_key={_config["ComicVineAPIKey"]}" +
                $"&format=json" +
                $"&offset={offset}" +
                $"&limit={pageSize}" +
                $"&filter=name:{Uri.EscapeDataString(searchQuery)}" +
                $"&field_list=id,name,publisher,start_year,count_of_issues,image,description";

            var json = await _comicVineConnection.Get(url);

            var fullResponse = JsonSerializer.Deserialize<JsonDocument>(json);
            var resultsJson = fullResponse.RootElement.GetProperty("results").GetRawText();
            var totalResults = fullResponse.RootElement.GetProperty("number_of_total_results").GetInt32();

            var volumes = JsonSerializer.Deserialize<List<VolumeDto>>(resultsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<VolumeDto>();

            return new PagedResult<VolumeDto>
            {
                Data = volumes,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalResults,
                TotalPages = (int)Math.Ceiling((double)totalResults / pageSize)
            };
        }
    }
}