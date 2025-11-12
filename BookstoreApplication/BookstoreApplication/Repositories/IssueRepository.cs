using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly AppDbContext _context;

        public IssueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Issue> CreateAsync(Issue issue)
        {
            _context.Issues.Add(issue);
            return issue;
        }

        public async Task<Issue?> GetByIdAsync(int id)
        {
            return await _context.Issues.FindAsync(id);
        }

        public async Task<List<Issue>> GetAllAsync()
        {
            return await _context.Issues.ToListAsync();
        }

        public async Task<bool> ExistsByExternalApiIdAsync(int externalApiId)
        {
            return await _context.Issues.AnyAsync(i => i.ExternalApiId == externalApiId);
        }
    }
}