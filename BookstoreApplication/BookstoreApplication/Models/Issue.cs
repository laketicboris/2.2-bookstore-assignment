namespace BookstoreApplication.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int IssueNumber { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public int ExternalApiId { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? VolumeApiDetailUrl { get; set; }
        public int? VolumeId { get; set; }
        public string? VolumeName { get; set; }
    }
}