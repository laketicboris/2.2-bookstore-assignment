using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IVolumeService
    {
        Task<PagedResult<VolumeDto>> SearchVolumesByName(string searchQuery, int page = 1, int pageSize = 10);
    }
}