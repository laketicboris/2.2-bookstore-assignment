using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookstoreApplication.Models
{
    public class IssueMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [BsonElement("issueNumber")]
        public int IssueNumber { get; set; }

        [BsonElement("imagePath")]
        public string? ImagePath { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("externalApiId")]
        public int ExternalApiId { get; set; }

        [BsonElement("pageCount")]
        public int PageCount { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("availableCopies")]
        public int AvailableCopies { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("volumeApiDetailUrl")]
        public string? VolumeApiDetailUrl { get; set; }

        [BsonElement("volumeId")]
        public int? VolumeId { get; set; }

        [BsonElement("volumeName")]
        public string? VolumeName { get; set; }

        public static IssueMongo FromIssue(Issue issue)
        {
            return new IssueMongo
            {
                Name = issue.Name,
                ReleaseDate = issue.ReleaseDate,
                IssueNumber = issue.IssueNumber,
                ImagePath = issue.ImagePath,
                Description = issue.Description,
                ExternalApiId = issue.ExternalApiId,
                PageCount = issue.PageCount,
                Price = issue.Price,
                AvailableCopies = issue.AvailableCopies,
                CreatedAt = issue.CreatedAt,
                VolumeApiDetailUrl = issue.VolumeApiDetailUrl,
                VolumeId = issue.VolumeId,
                VolumeName = issue.VolumeName
            };
        }
        public Issue ToIssue()
        {
            return new Issue
            {
                Id = int.TryParse(Id, out var intId) ? intId : 0,
                Name = Name,
                ReleaseDate = ReleaseDate,
                IssueNumber = IssueNumber,
                ImagePath = ImagePath,
                Description = Description,
                ExternalApiId = ExternalApiId,
                PageCount = PageCount,
                Price = Price,
                AvailableCopies = AvailableCopies,
                CreatedAt = CreatedAt,
                VolumeApiDetailUrl = VolumeApiDetailUrl,
                VolumeId = VolumeId,
                VolumeName = VolumeName
            };
        }
    }
}