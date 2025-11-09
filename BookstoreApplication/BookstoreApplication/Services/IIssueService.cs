using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IIssueService
    {
        Task<List<IssueDto>> SearchIssuesByVolume(int volumeId);
        Task<int> CreateIssueAsync(SaveIssueDto saveIssueDto);
        Task<IssueDto> GetIssueByIdAsync(int id);
    }
}