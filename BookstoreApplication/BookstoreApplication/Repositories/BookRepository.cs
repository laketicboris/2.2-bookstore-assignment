using BookstoreApplication.Models;
using BookstoreApplication.DTOs;
using Microsoft.EntityFrameworkCore;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Book>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            IQueryable<Book> query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);

            return await query.ToPagedResultAsync(page, pageSize);
        }

        public async Task<PagedResult<Book>> GetAllSortedAsync(BookSortType sortType, int page = 1, int pageSize = 10)
        {
            IQueryable<Book> query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);

            query = sortType switch
            {
                BookSortType.TitleAscending => query.OrderBy(b => b.Title),
                BookSortType.TitleDescending => query.OrderByDescending(b => b.Title),
                BookSortType.PublishedDateAscending => query.OrderBy(b => b.PublishedDate),
                BookSortType.PublishedDateDescending => query.OrderByDescending(b => b.PublishedDate),
                BookSortType.AuthorNameAscending => query.OrderBy(b => b.Author!.FullName),
                BookSortType.AuthorNameDescending => query.OrderByDescending(b => b.Author!.FullName),
                _ => query.OrderBy(b => b.Title)
            };

            return await query.ToPagedResultAsync(page, pageSize);
        }

        public async Task<PagedResult<Book>> GetAllFilteredAndSortedAsync(BookFilterDto filter, BookSortType sortType, int page = 1, int pageSize = 10)
        {
            IQueryable<Book> query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);

            query = ApplyFilters(query, filter);
            query = ApplySorting(query, sortType);

            return await query.ToPagedResultAsync(page, pageSize);
        }

        private static IQueryable<Book> ApplyFilters(IQueryable<Book> query, BookFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(b => b.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if (filter.PublishedDateFrom.HasValue)
            {
                query = query.Where(b => b.PublishedDate >= filter.PublishedDateFrom.Value);
            }

            if (filter.PublishedDateTo.HasValue)
            {
                query = query.Where(b => b.PublishedDate <= filter.PublishedDateTo.Value);
            }

            if (filter.AuthorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == filter.AuthorId.Value);
            }

            if (!string.IsNullOrEmpty(filter.AuthorName))
            {
                query = query.Where(b => b.Author!.FullName.ToLower().Contains(filter.AuthorName.ToLower()));
            }

            if (filter.AuthorBirthDateFrom.HasValue)
            {
                query = query.Where(b => b.Author!.DateOfBirth >= filter.AuthorBirthDateFrom.Value);
            }

            if (filter.AuthorBirthDateTo.HasValue)
            {
                query = query.Where(b => b.Author!.DateOfBirth <= filter.AuthorBirthDateTo.Value);
            }

            return query;
        }

        private static IQueryable<Book> ApplySorting(IQueryable<Book> query, BookSortType sortType)
        {
            return sortType switch
            {
                BookSortType.TitleAscending => query.OrderBy(b => b.Title),
                BookSortType.TitleDescending => query.OrderByDescending(b => b.Title),
                BookSortType.PublishedDateAscending => query.OrderBy(b => b.PublishedDate),
                BookSortType.PublishedDateDescending => query.OrderByDescending(b => b.PublishedDate),
                BookSortType.AuthorNameAscending => query.OrderBy(b => b.Author!.FullName),
                BookSortType.AuthorNameDescending => query.OrderByDescending(b => b.Author!.FullName),
                _ => query.OrderBy(b => b.Title)
            };
        }

        public List<BookSortTypeOption> GetSortTypes()
        {
            List<BookSortTypeOption> options = new List<BookSortTypeOption>();
            var enumValues = Enum.GetValues(typeof(BookSortType));

            foreach (BookSortType sortType in enumValues)
            {
                options.Add(new BookSortTypeOption(sortType));
            }

            return options;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> CreateAsync(Book book)
        {
            var author = await _context.Authors.FindAsync(book.AuthorId);
            if (author == null)
                return null;

            var publisher = await _context.Publishers.FindAsync(book.PublisherId);
            if (publisher == null)
                return null;

            _context.Books.Add(book);

            return await GetByIdAsync(book.Id);
        }

        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                return null;

            var author = await _context.Authors.FindAsync(book.AuthorId);
            if (author == null)
                return null;

            var publisher = await _context.Publishers.FindAsync(book.PublisherId);
            if (publisher == null)
                return null;

            existingBook.Title = book.Title;
            existingBook.PageCount = book.PageCount;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.AuthorId = book.AuthorId;
            existingBook.PublisherId = book.PublisherId;

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            return true;
        }
    }
}