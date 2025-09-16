using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Award> Award { get; set; }
        public DbSet<AuthorAward> AuthorAward { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorAward>(entity =>
            {
                entity.ToTable("AuthorAwardBridge");

                entity.HasKey(authorAward => new { authorAward.AuthorId, authorAward.AwardId });

                entity.HasOne(authorAward => authorAward.Author)
                    .WithMany(author => author.AuthorAwards)
                    .HasForeignKey(authorAward => authorAward.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(authorAward => authorAward.Award)
                    .WithMany(award => award.AuthorAwards)
                    .HasForeignKey(authorAward => authorAward.AwardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(author => author.DateOfBirth)
                    .HasColumnName("Birthday");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(book => book.Publisher)
                    .WithMany()
                    .HasForeignKey(book => book.PublisherId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(book => book.Author)
                    .WithMany()
                    .HasForeignKey(book => book.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}