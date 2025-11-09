using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IVolumeService
    {
        Task<List<VolumeDto>> SearchVolumesByName(string searchQuery);
    }
}