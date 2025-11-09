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

        public async Task<List<VolumeDto>> SearchVolumesByName(string searchQuery)
        {
            _logger.LogInformation("Searching volumes for query: {Query}", searchQuery);

            var url = $"{_config["ComicVineBaseUrl"]}/volumes" +
              $"?api_key={_config["ComicVineAPIKey"]}" +
              $"&format=json" +
              $"&filter=name:{Uri.EscapeDataString(searchQuery)}" +
              $"&field_list=id,name,publisher,start_year,count_of_issues,image,description";

            var json = await _comicVineConnection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var volumes = JsonSerializer.Deserialize<List<VolumeDto>>(json, options) ?? new List<VolumeDto>();

            _logger.LogInformation("Found {Count} volumes for query: {Query}", volumes.Count, searchQuery);

            return volumes;
        }
    }
}