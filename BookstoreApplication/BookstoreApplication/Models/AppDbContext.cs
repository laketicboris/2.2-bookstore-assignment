using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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
                entity.HasKey(authorAward => new { authorAward.AuthorId, authorAward.AwardId });
                
                entity.HasOne(authorAward => authorAward.Author)
                .WithMany(author => author.AuthorAwards)
                .HasForeignKey(authorAward => authorAward.AuthorId);

                entity.HasOne(authorAward => authorAward.Award)
                .WithMany()
                .HasForeignKey(authorAward => authorAward.AwardId);
            });
        }


    }
}
