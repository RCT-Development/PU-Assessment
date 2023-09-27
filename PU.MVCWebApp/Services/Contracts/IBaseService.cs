namespace PU.MVCWebApp.Services.Contracts
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<HttpResponseMessage> Update(T entity);
        Task<HttpResponseMessage> Create(T entity);
        Task<HttpResponseMessage> Delete(Guid id);
    }
}
