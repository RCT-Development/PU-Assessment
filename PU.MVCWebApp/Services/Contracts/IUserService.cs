using PU.Core.Models;

namespace PU.MVCWebApp.Services.Contracts
{
    public interface IUserService : IBaseService<User>
    {
        Task<int> GetUserCount();
    }
}
