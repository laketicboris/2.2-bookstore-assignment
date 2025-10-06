using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class PublisherService
    {
        private readonly PublisherRepository _repository;

        public PublisherService(PublisherRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            List<Publisher> publishers = await _repository.GetAllAsync();
            return publishers;
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            Publisher? publisher = await _repository.GetByIdAsync(id);
            return publisher;
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            Publisher createdPublisher = await _repository.CreateAsync(publisher);
            return createdPublisher;
        }

        public async Task<Publisher?> UpdateAsync(int id, Publisher publisher)
        {
            Publisher? updatedPublisher = await _repository.UpdateAsync(id, publisher);
            return updatedPublisher;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = await _repository.DeleteAsync(id);
            return deleted;
        }
    }
}