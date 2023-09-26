using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PU.Core.DTO.Request;
using PU.Core.DTO.Response;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;


namespace PU.DataAccess.Repositories
{
    public class GroupRepository : RepositoryBase<Group, Entities.Group>, IGroupRepository
    {

        public GroupRepository(IConfigurationProvider configurationProvider,
                               IMapper mapper,
                               PURepositoryContext context) : base(configurationProvider, mapper, context)
        {
        }

        public async Task<IEnumerable<GroupUserCount>> GetUserCountPerGroup()
        {
            return await Context.UserGroup
                .GroupBy(x => x.Group)
                .Select(x => new GroupUserCount
                {
                    GroupName = x.Key.Name,
                    UserCount = x.Count()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetGroupUsers(Guid groupId)
        {
            var groupUsers = await Context.UserGroup
                            .Where(x => x.GroupId == groupId)
                            .Select(x => x.User)
                            .ToListAsync();

            return groupUsers.Select(x => Mapper.Map<User>(x));                          
                            
        }
        public async Task AddGroupUser(Guid groupId, Guid userId)
        {
            var userGroup = new Entities.UserGroup 
            {
                GroupId = groupId,
                UserId = userId
            };

            Context.UserGroup.Add(userGroup);
            await Context.SaveChangesAsync();
        }
        public async Task RemoveGroupUser(Guid groupId, Guid userId)
        {
            var userGroup = await Context.UserGroup.FirstOrDefaultAsync(x => x.GroupId == groupId && x.UserId ==  userId);

            if (userGroup == null)
            {
                throw new NotFoundException($"User: {userId} does not exist in Group: {groupId}");
            }

            Context.UserGroup.Remove(userGroup);
            await Context.SaveChangesAsync();

        }
        public async Task<IEnumerable<Permission>> GetGroupPermissions(Guid groupId)
        {
            var groupPermissions = await Context.GroupPermission
                                            .Where(x => x.GroupId == groupId)
                                            .Select(x => x.Permission)
                                            .ToListAsync();

            return groupPermissions.Select(x => Mapper.Map<Permission>(x));
        }
        public async Task AddGroupPermission(Guid groupId, Guid permissionId)
        {
            var groupPermission = new Entities.GroupPermission
            {
                GroupId = groupId,
                PermissionId = permissionId
            };

            Context.GroupPermission.Add(groupPermission);
            await Context.SaveChangesAsync();
        }
        public async Task RemoveGroupPermission(Guid groupId, Guid permissionId)
        {
            var groupPermission = await Context.GroupPermission.FirstOrDefaultAsync(x => x.GroupId == groupId && x.PermissionId == permissionId);

            if (groupPermission == null)
            {
                throw new NotFoundException($"Permission: {permissionId} does not exist in Group: {groupId}");
            }

            Context.GroupPermission.Remove(groupPermission);
            await Context.SaveChangesAsync();
        }
    }
}
