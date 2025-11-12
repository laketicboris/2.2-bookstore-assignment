using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IReviewService
    {
        Task<Review> CreateReviewAsync(NewReviewDto newReviewDto, string? userId);
        Task<List<Review>> GetReviewsByBookIdAsync(int bookId);
        Task<List<Review>> GetReviewsByUserIdAsync(string userId);
        Task<Review> GetReviewByIdAsync(int id);
        Task<bool> HasUserReviewedBookAsync(int bookId, string userId);
        Task<ReviewDto?> GetUserReviewForBookAsync(int bookId, string userId);
        Task<List<ReviewDto>> GetReviewsWithUserInfoAsync(int bookId);
    }
}