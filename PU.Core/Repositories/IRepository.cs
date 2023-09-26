using System.Linq.Expressions;

namespace PU.Core.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}
