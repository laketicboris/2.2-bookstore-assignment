using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IIssueService
    {
        Task<PagedResult<IssueDto>> SearchIssuesByVolume(int volumeId, int page = 1, int pageSize = 10);
        Task<int> CreateIssueAsync(SaveIssueDto saveIssueDto);
        Task<IssueDto> GetIssueByIdAsync(int id);
    }
}