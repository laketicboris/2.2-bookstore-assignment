using BookstoreApplication.Models;

namespace BookstoreApplication.Models
{
    public interface IIssueRepository
    {
        Task<Issue> CreateAsync(Issue issue);
        Task<Issue?> GetByIdAsync(int id);
        Task<List<Issue>> GetAllAsync();
        Task<bool> ExistsByExternalApiIdAsync(int externalApiId);
    }
}