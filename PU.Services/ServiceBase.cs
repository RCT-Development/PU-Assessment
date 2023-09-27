using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;
using PU.Core.Services.Contracts;

namespace PU.Services
{
    public class ServiceBase<Model> : IBaseService<Model> where Model : ModelBase
    {
        private readonly IRepository<Model> _repository;

        public ServiceBase(IRepository<Model> repository)
        {
            _repository = repository;
        }

        public IRepository<Model> Repository => _repository;

        public async Task CreateAsync(Model item)
        {
            await _repository.CreateAsync(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _repository.GetAsync(id);
            if (item == null)
            {
                throw new NotFoundException($"item: {id} does not exist");
            }
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Model> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task UpdateAsync(Model item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
