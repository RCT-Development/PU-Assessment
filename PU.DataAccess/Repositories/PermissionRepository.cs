using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PU.Core.DTO.Request;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;

namespace PU.DataAccess.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission, Entities.Permission>, IPermissionRepository
    {

        public PermissionRepository(IConfigurationProvider configurationProvider,
                                    IMapper mapper,
                                    PURepositoryContext context) : base(configurationProvider, mapper, context)
        {
        }
    }
}
