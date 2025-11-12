using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using BookstoreApplication.Repositories;


namespace BookstoreApplication.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly AppDbContext _context;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(
            IUnitOfWork unitOfWork,
            IReviewRepository reviewRepository,
            IBookRepository bookRepository,
            AppDbContext context,
            ILogger<ReviewService> logger)
        {
            _unitOfWork = unitOfWork;
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _context = context;
            _logger = logger;
        }

        public async Task<Review> CreateReviewAsync(NewReviewDto newReviewDto, string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Unauthorized attempt to create review - no user ID in token");
                throw new AuthenticationException("User ID not found in token");
            }

            if (newReviewDto.Rating < 1 || newReviewDto.Rating > 5)
            {
                throw new BadRequestException("Rating must be between 1 and 5");
            }

            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.BookId == newReviewDto.BookId && r.UserId == userId);

            if (existingReview != null)
            {
                throw new BadRequestException("You have already reviewed this book. You can only review a book once.");
            }

            _logger.LogInformation("Creating review for book {BookId} by user {UserId}", newReviewDto.BookId, userId);

            var book = await _bookRepository.GetByIdAsync(newReviewDto.BookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {newReviewDto.BookId} not found");
            }

            var review = new Review
            {
                BookId = newReviewDto.BookId,
                UserId = userId,
                Rating = newReviewDto.Rating,
                Comment = newReviewDto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _reviewRepository.Add(review);
                var averageRating = await _reviewRepository.CalculateAverageRatingForBookAsync(newReviewDto.BookId);
                book.AverageRating = (decimal)averageRating;
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("Review created successfully with ID: {ReviewId}", review.Id);
                return review;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating review for book {BookId}", newReviewDto.BookId);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Review>> GetReviewsByBookIdAsync(int bookId)
        {
            if (bookId <= 0)
            {
                throw new BadRequestException("Book ID must be a positive number");
            }
            return await _reviewRepository.GetByBookIdAsync(bookId);
        }

        public async Task<List<Review>> GetReviewsByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new BadRequestException("User ID cannot be empty");
            }
            return await _reviewRepository.GetByUserIdAsync(userId);
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Review ID must be a positive number");
            }
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
            {
                throw new NotFoundException($"Review with ID {id} not found");
            }
            return review;
        }

        public async Task<bool> HasUserReviewedBookAsync(int bookId, string userId)
        {
            if (bookId <= 0 || string.IsNullOrEmpty(userId))
            {
                return false;
            }

            return await _context.Reviews
                .AnyAsync(r => r.BookId == bookId && r.UserId == userId);
        }

        public async Task<ReviewDto?> GetUserReviewForBookAsync(int bookId, string userId)
        {
            if (bookId <= 0 || string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var review = await _context.Reviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.BookId == bookId && r.UserId == userId);

            if (review == null)
            {
                return null;
            }

            return new ReviewDto
            {
                Id = review.Id,
                BookId = review.BookId,
                UserId = review.UserId,
                UserName = review.User?.UserName ?? "Unknown",
                UserFullName = review.User != null ? $"{review.User.Name} {review.User.Surname}" : "Unknown User",
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }

        public async Task<List<ReviewDto>> GetReviewsWithUserInfoAsync(int bookId)
        {
            if (bookId <= 0)
            {
                throw new BadRequestException("Book ID must be a positive number");
            }

            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.BookId == bookId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return reviews.Select(review => new ReviewDto
            {
                Id = review.Id,
                BookId = review.BookId,
                UserId = review.UserId,
                UserName = review.User?.UserName ?? "Unknown",
                UserFullName = review.User != null ? $"{review.User.Name} {review.User.Surname}" : "Unknown User",
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            }).ToList();
        }
    }
}