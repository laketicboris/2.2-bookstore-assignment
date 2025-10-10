using BookstoreApplication.Models;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;

namespace BookstoreApplication.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repository;

        public PublisherService(IPublisherRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            List<Publisher> publishers = await _repository.GetAllAsync();
            return publishers;
        }

        public async Task<List<Publisher>> GetAllSortedAsync(PublisherSortType sortType)
        {
            List<Publisher> publishers = await _repository.GetAllSortedAsync(sortType);
            return publishers;
        }

        public List<SortTypeOption> GetSortTypes()
        {
            return _repository.GetSortTypes();
        }

        public async Task<Publisher> GetByIdAsync(int id)
        {
            Publisher? publisher = await _repository.GetByIdAsync(id);
            if (publisher == null)
            {
                throw new NotFoundException(id);
            }
            return publisher;
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            Publisher createdPublisher = await _repository.CreateAsync(publisher);
            return createdPublisher;
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                throw new BadRequestException("ID in URL does not match ID in body");
            }

            Publisher? updatedPublisher = await _repository.UpdateAsync(id, publisher);
            if (updatedPublisher == null)
            {
                throw new NotFoundException(id);
            }

            return updatedPublisher;
        }

        public async Task DeleteAsync(int id)
        {
            bool deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException(id);
            }
        }
    }
}