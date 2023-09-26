using PU.Core.DTO.Request;
using PU.Core.Models;

namespace PU.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(Guid id);
        Task CreateUser(User model);
        Task UpdateUser(User model);
        Task DeleteUser(Guid id);
    }
}
