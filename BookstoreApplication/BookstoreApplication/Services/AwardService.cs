using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardRepository _repository;

        public AwardService(IAwardRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Award>> GetAllAsync()
        {
            List<Award> awards = await _repository.GetAllAsync();
            return awards;
        }

        public async Task<Award?> GetByIdAsync(int id)
        {
            Award? award = await _repository.GetByIdAsync(id);
            return award;
        }

        public async Task<Award> CreateAsync(Award award)
        {
            Award createdAward = await _repository.CreateAsync(award);
            return createdAward;
        }

        public async Task<Award?> UpdateAsync(int id, Award award)
        {
            Award? updatedAward = await _repository.UpdateAsync(id, award);
            return updatedAward;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = await _repository.DeleteAsync(id);
            return deleted;
        }
    }
}