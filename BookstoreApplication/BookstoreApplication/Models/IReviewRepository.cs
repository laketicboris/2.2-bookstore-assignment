using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task<List<Review>> GetByBookIdAsync(int bookId);
        Task<List<Review>> GetByUserIdAsync(string userId);
        void Add(Review review);
        void Update(Review review);
        void Remove(Review review);
        Task<double> CalculateAverageRatingForBookAsync(int bookId);
        Task<Review?> GetByBookAndUserAsync(int bookId, string userId);
        Task<List<Review>> GetByBookIdWithUserAsync(int bookId);
    }
}