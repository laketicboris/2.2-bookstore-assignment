using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.DTOs
{
    public class SaveIssueDto
    {
        [Required]
        public int ExternalApiId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Available copies cannot be negative")]
        public int AvailableCopies { get; set; }

        public string? AdditionalNotes { get; set; }
    }
}