using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AuthorAward> AuthorAwards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorAward>()
                .ToTable("AuthorAwardBridge");

            modelBuilder.Entity<AuthorAward>()
                .HasKey(authorAward => new { authorAward.AuthorId, authorAward.AwardId });

            modelBuilder.Entity<AuthorAward>()
                .HasOne(authorAward => authorAward.Author)
                .WithMany(author => author.AuthorAwards)
                .HasForeignKey(authorAward => authorAward.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuthorAward>()
                .HasOne(authorAward => authorAward.Award)
                .WithMany(award => award.AuthorAwards)
                .HasForeignKey(authorAward => authorAward.AwardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>()
                .Property(author => author.DateOfBirth)
                .HasColumnName("Birthday");

            modelBuilder.Entity<Book>()
                .HasOne(book => book.Publisher)
                .WithMany()
                .HasForeignKey(book => book.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(book => book.Author)
                .WithMany()
                .HasForeignKey(book => book.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "Agatha Christie", Biography = "British crime novelist known for detective stories", DateOfBirth = new DateTime(1890, 9, 15, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 2, FullName = "Stephen King", Biography = "American author of horror and supernatural fiction", DateOfBirth = new DateTime(1947, 9, 21, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 3, FullName = "J.K. Rowling", Biography = "British author best known for the Harry Potter series", DateOfBirth = new DateTime(1965, 7, 31, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 4, FullName = "George Orwell", Biography = "English novelist and essayist", DateOfBirth = new DateTime(1903, 6, 25, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 5, FullName = "Harper Lee", Biography = "American novelist known for To Kill a Mockingbird", DateOfBirth = new DateTime(1926, 4, 28, 0, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Penguin Random House", Address = "1745 Broadway, New York, NY", Website = "https://www.penguinrandomhouse.com" },
                new Publisher { Id = 2, Name = "HarperCollins", Address = "195 Broadway, New York, NY", Website = "https://www.harpercollins.com" },
                new Publisher { Id = 3, Name = "Simon & Schuster", Address = "1230 Avenue of the Americas, NY", Website = "https://www.simonandschuster.com" }
            );

            modelBuilder.Entity<Award>().HasData(
                new Award { Id = 1, Name = "Hugo Award", Description = "Award for best science fiction or fantasy works", StartYear = 1953 },
                new Award { Id = 2, Name = "Edgar Award", Description = "Award for outstanding work in mystery genre", StartYear = 1946 },
                new Award { Id = 3, Name = "Pulitzer Prize", Description = "Award for distinguished fiction", StartYear = 1918 },
                new Award { Id = 4, Name = "Nebula Award", Description = "Award by Science Fiction Writers of America", StartYear = 1965 }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Murder on the Orient Express", PageCount = 256, PublishedDate = new DateTime(1934, 1, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-00-711930-6", AuthorId = 1, PublisherId = 2 },
                new Book { Id = 2, Title = "The ABC Murders", PageCount = 272, PublishedDate = new DateTime(1936, 1, 6, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-00-711931-3", AuthorId = 1, PublisherId = 2 },
                new Book { Id = 3, Title = "Death on the Nile", PageCount = 288, PublishedDate = new DateTime(1937, 11, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-00-711932-0", AuthorId = 1, PublisherId = 2 },
                new Book { Id = 4, Title = "The Shining", PageCount = 447, PublishedDate = new DateTime(1977, 1, 28, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-385-12167-5", AuthorId = 2, PublisherId = 1 },
                new Book { Id = 5, Title = "It", PageCount = 1138, PublishedDate = new DateTime(1986, 9, 15, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-670-81302-4", AuthorId = 2, PublisherId = 1 },
                new Book { Id = 6, Title = "Carrie", PageCount = 199, PublishedDate = new DateTime(1974, 4, 5, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-385-08695-0", AuthorId = 2, PublisherId = 1 },
                new Book { Id = 7, Title = "Harry Potter and the Philosopher's Stone", PageCount = 223, PublishedDate = new DateTime(1997, 6, 26, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-7475-3269-9", AuthorId = 3, PublisherId = 1 },
                new Book { Id = 8, Title = "Harry Potter and the Chamber of Secrets", PageCount = 251, PublishedDate = new DateTime(1998, 7, 2, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-7475-3849-3", AuthorId = 3, PublisherId = 1 },
                new Book { Id = 9, Title = "Harry Potter and the Prisoner of Azkaban", PageCount = 317, PublishedDate = new DateTime(1999, 7, 8, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-7475-4215-5", AuthorId = 3, PublisherId = 1 },
                new Book { Id = 10, Title = "1984", PageCount = 328, PublishedDate = new DateTime(1949, 6, 8, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-452-28423-4", AuthorId = 4, PublisherId = 3 },
                new Book { Id = 11, Title = "Animal Farm", PageCount = 112, PublishedDate = new DateTime(1945, 8, 17, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-452-28424-1", AuthorId = 4, PublisherId = 3 },
                new Book { Id = 12, Title = "To Kill a Mockingbird", PageCount = 376, PublishedDate = new DateTime(1960, 7, 11, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0-06-112008-4", AuthorId = 5, PublisherId = 2 }
            );

            modelBuilder.Entity<AuthorAward>().HasData(
                new AuthorAward { AuthorId = 1, AwardId = 2, YearAwarded = 1955 },
                new AuthorAward { AuthorId = 1, AwardId = 3, YearAwarded = 1956 },
                new AuthorAward { AuthorId = 1, AwardId = 4, YearAwarded = 1962 },
                new AuthorAward { AuthorId = 2, AwardId = 1, YearAwarded = 1982 },
                new AuthorAward { AuthorId = 2, AwardId = 2, YearAwarded = 2009 },
                new AuthorAward { AuthorId = 2, AwardId = 3, YearAwarded = 1996 },
                new AuthorAward { AuthorId = 2, AwardId = 4, YearAwarded = 2015 },
                new AuthorAward { AuthorId = 3, AwardId = 1, YearAwarded = 2001 },
                new AuthorAward { AuthorId = 3, AwardId = 2, YearAwarded = 2004 },
                new AuthorAward { AuthorId = 3, AwardId = 3, YearAwarded = 2017 },
                new AuthorAward { AuthorId = 4, AwardId = 1, YearAwarded = 1984 },
                new AuthorAward { AuthorId = 4, AwardId = 2, YearAwarded = 1949 },
                new AuthorAward { AuthorId = 5, AwardId = 3, YearAwarded = 1961 },
                new AuthorAward { AuthorId = 5, AwardId = 4, YearAwarded = 1961 },
                new AuthorAward { AuthorId = 3, AwardId = 4, YearAwarded = 1971 }
            );
        }
    }
}