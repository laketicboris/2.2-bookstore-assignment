namespace BookstoreApplication.Models
{
    public interface IAwardRepository
    {
        Task<List<Award>> GetAllAsync();
        Task<Award?> GetByIdAsync(int id);
        Task<Award> CreateAsync(Award award);
        Task<Award?> UpdateAsync(int id, Award award);
        Task<bool> DeleteAsync(int id);
    }
}