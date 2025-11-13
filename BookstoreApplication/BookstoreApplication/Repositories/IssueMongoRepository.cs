using MongoDB.Driver;
using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories
{
    public class IssueMongoRepository : IIssueRepository
    {
        private readonly IMongoCollection<IssueMongo> _issues;
        private readonly ILogger<IssueMongoRepository> _logger;

        public IssueMongoRepository(IMongoDatabase database, ILogger<IssueMongoRepository> logger)
        {
            _issues = database.GetCollection<IssueMongo>("issues");
            _logger = logger;
        }

        public async Task<Issue> CreateAsync(Issue issue)
        {
            try
            {
                var issueMongo = IssueMongo.FromIssue(issue);
                await _issues.InsertOneAsync(issueMongo);

                _logger.LogInformation("Issue created in MongoDB with ExternalApiId: {ExternalApiId}", issueMongo.ExternalApiId);

                var result = issueMongo.ToIssue();
                result.Id = issueMongo.ExternalApiId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating issue in MongoDB");
                throw;
            }
        }

        public async Task<Issue?> GetByIdAsync(int id)
        {
            try
            {
                var issue = await _issues.Find(i => i.ExternalApiId == id).FirstOrDefaultAsync();

                if (issue == null)
                    return null;

                var result = issue.ToIssue();
                result.Id = issue.ExternalApiId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting issue by ID: {Id}", id);
                throw;
            }
        }

        public async Task<List<Issue>> GetAllAsync()
        {
            try
            {
                var issues = await _issues.Find(_ => true)
                                         .SortByDescending(i => i.CreatedAt)
                                         .ToListAsync();

                return issues.Select(i => i.ToIssue()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all issues from MongoDB");
                throw;
            }
        }

        public async Task<bool> ExistsByExternalApiIdAsync(int externalApiId)
        {
            try
            {
                var count = await _issues.CountDocumentsAsync(i => i.ExternalApiId == externalApiId);
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if issue exists by ExternalApiId: {ExternalApiId}", externalApiId);
                throw;
            }
        }
    }
}