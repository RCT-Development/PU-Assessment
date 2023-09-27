using PU.Core.DTO.Request;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;
using PU.Core.Services.Contracts;

namespace PU.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            
        }
    }
}
