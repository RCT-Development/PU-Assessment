using AutoMapper;
using PU.Core.Models;
using PU.Core.Repositories;

namespace PU.DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User, Entities.User>, IUserRepository
    {
        public UserRepository(IConfigurationProvider configurationProvider,
                              IMapper mapper, 
                              PURepositoryContext context) : base(configurationProvider, mapper, context)
        {
        }
    }
}
