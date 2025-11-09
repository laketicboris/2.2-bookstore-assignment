using System.Text.Json.Serialization;

namespace BookstoreApplication.DTOs
{
    public class IssueDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("issue_number")]
        public string? IssueNumberString { get; set; }

        [JsonPropertyName("cover_date")]
        public DateTime? CoverDate { get; set; }

        public IssueImageDto? Image { get; set; }
        public string? Description { get; set; }

        [JsonPropertyName("page_count")]
        public int? PageCount { get; set; }

        public IssueVolumeDto? Volume { get; set; }

        [JsonIgnore]
        public int? IssueNumber => int.TryParse(IssueNumberString, out var num) ? num : null;

        [JsonIgnore]
        public string? ImageUrl => Image?.MediumUrl;

        [JsonIgnore]
        public string? VolumeName => Volume?.Name;
    }

    public class IssueImageDto
    {
        [JsonPropertyName("medium_url")]
        public string? MediumUrl { get; set; }
    }

    public class IssueVolumeDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        [JsonPropertyName("api_detail_url")]
        public string? ApiDetailUrl { get; set; }
    }
}