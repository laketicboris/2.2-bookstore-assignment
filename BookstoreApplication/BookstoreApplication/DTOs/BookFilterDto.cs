namespace BookstoreApplication.DTOs
{
    public class BookFilterDto
    {
        // Pretraga po nazivu knjige
        public string? Title { get; set; }

        // Pretraga po datumu izdavanja knjige
        public DateTime? PublishedDateFrom { get; set; }
        public DateTime? PublishedDateTo { get; set; }

        // Pretraga po autoru knjige
        public int? AuthorId { get; set; }

        // Pretraga po imenu autora
        public string? AuthorName { get; set; }

        // Pretraga po datumu rodjenja autora
        public DateTime? AuthorBirthDateFrom { get; set; }
        public DateTime? AuthorBirthDateTo { get; set; }
    }
}