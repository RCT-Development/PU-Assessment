using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PU.Core.Models;
using PU.Core.Repositories;
using PU.DataAccess.Entities;

namespace PU.DataAccess.Repositories
{
    public class RepositoryBase<Model, Entity> : IRepository<Model>
        where Model : ModelBase
        where Entity : EntityBase
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;
        private readonly PURepositoryContext _context;

        public RepositoryBase(IConfigurationProvider configurationProvider,
                              IMapper mapper,
                              PURepositoryContext context)
        {
            _configurationProvider = configurationProvider;
            _mapper = mapper;
            _context = context;
        }

        public IConfigurationProvider ConfigurationProvider => _configurationProvider;
        public IMapper Mapper => _mapper;
        public PURepositoryContext Context => _context;

        public async Task<Model> GetAsync(Guid id)
        {
            return await _context.Set<Entity>()
                                 .Where(i => i.Id == id)
                                 .ProjectTo<Model>(_configurationProvider)
                                 .FirstOrDefaultAsync()
                                 .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _context.Set<Entity>()
                                 .ProjectTo<Model>(_configurationProvider)
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }

        public async Task<Model> CreateAsync(Model item)
        {
            item.CreatedDate = DateTime.UtcNow;
            var entityToAdd = _mapper.Map<Entity>(item);

            _context.Set<Entity>().Add(entityToAdd);
            await _context.SaveChangesAsync()
                          .ConfigureAwait(false);

            return item;
        }

        public async Task UpdateAsync(Model item)
        {
            item.UpdatedDate = DateTime.UtcNow;
            var entityToUpdate = _mapper.Map<Entity>(item);

            var existingItemInContext = await _context.Set<Entity>().FindAsync(entityToUpdate.Id).ConfigureAwait(false);
            if (existingItemInContext == null)
            {
                return;
            }

            _context.Entry(existingItemInContext).CurrentValues.SetValues(entityToUpdate);
            _context.Entry(existingItemInContext).State = EntityState.Modified;

            await _context.SaveChangesAsync()
                          .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Model item)
        {
            var entityToDelete = _mapper.Map<Entity>(item);

            _context.Entry(entityToDelete).State = EntityState.Deleted;
            await _context.SaveChangesAsync()
                          .ConfigureAwait(false);
        }
    }
}
