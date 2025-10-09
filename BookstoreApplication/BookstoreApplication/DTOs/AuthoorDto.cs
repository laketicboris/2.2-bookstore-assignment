namespace BookstoreApplication.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}