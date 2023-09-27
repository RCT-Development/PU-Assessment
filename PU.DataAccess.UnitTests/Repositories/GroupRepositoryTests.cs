using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using PU.Core.Exceptions;
using PU.DataAccess.Entities;
using PU.DataAccess.Repositories;

namespace PU.DataAccess.UnitTests.Repositories
{
    [TestFixture]
    public class GroupRepositoryTests
    {
        private GroupRepository _sut;
        private Mock<IConfigurationProvider> _configurationProviderMock;
        private Mock<IMapper> _mapperMock;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _configurationProviderMock = new Mock<IConfigurationProvider>();
            var contextOptions = new DbContextOptionsBuilder<PURepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new PURepositoryContext(contextOptions);
            _sut = new GroupRepository(_configurationProviderMock.Object, _mapperMock.Object, context);
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task GetUserCountPerGroup_ReturnsGroupUserCounts()
        {
            var groupUsers = _fixture.CreateMany<UserGroup>();

            _sut.Context.UserGroup.AddRange(groupUsers);
            await _sut.Context.SaveChangesAsync();

            var result = await _sut.GetUserCountPerGroup();

            var testGroup = groupUsers.First();
            var expectedUserCount = groupUsers.Where(x => x.GroupId == testGroup?.GroupId).Count();
            Assert.That(result.Where(x => x.GroupName == testGroup?.Group?.Name).Count, Is.EqualTo(expectedUserCount));
        }

        [Test]
        public async Task GetGroupUsers_ReturnsUsersInGroup()
        {
            var group = _fixture.Create<Group>();
            _sut.Context.Group.AddRange(group);
            await _sut.Context.SaveChangesAsync();

            var result = await _sut.GetGroupUsers(group.Id);

            Assert.That(result.Count, Is.EqualTo(group.UserGroups.Count()));
        }

        [Test]
        public async Task AddGroupUser_ShouldAddUserToGroup()
        {
            var group = _fixture.Create<Group>();
            var user = _fixture.Create<User>();
            
            _sut.Context.User.Add(user);
            _sut.Context.Group.Add(group);
            await _sut.Context.SaveChangesAsync();

            await _sut.AddGroupUser(group.Id, user.Id);

            Assert.That(_sut.Context.UserGroup.FirstOrDefault(x => x.GroupId == group.Id && x.UserId == user.Id), Is.Not.EqualTo(null));
        }

        [Test]
        public async Task RemoveGroupUser_ExistingUserGroup_ShouldRemoveUserToGroup()
        {
            var group = _fixture.Create<Group>();
            var user = _fixture.Create<User>();
            var userGroup = new UserGroup
            {
                GroupId = group.Id,
                UserId = user.Id
            };

            _sut.Context.User.Add(user);
            _sut.Context.Group.Add(group);
            _sut.Context.UserGroup.Add(userGroup);
            await _sut.Context.SaveChangesAsync();


            await _sut.RemoveGroupUser(group.Id, user.Id);

            Assert.That(_sut.Context.UserGroup.FirstOrDefault(x => x.GroupId == group.Id && x.UserId == user.Id), Is.EqualTo(null));
        }

        [Test]
        public async Task RemoveGroupUser_InvalidUserGroup_ShouldRemoveUserToGroup()
        {
            var group = _fixture.Create<Group>();
            var user = _fixture.Create<User>();

            _sut.Context.User.Add(user);
            _sut.Context.Group.Add(group);
            await _sut.Context.SaveChangesAsync();

            Assert.ThrowsAsync<NotFoundException>(() => _sut.RemoveGroupUser(group.Id, user.Id));
        }

        [Test]
        public async Task GetGroupPermissions_ReturnsPermissionsInGroup()
        {
            var group = _fixture.Create<Group>();
            _sut.Context.Group.AddRange(group);
            await _sut.Context.SaveChangesAsync();

            var result = await _sut.GetGroupPermissions(group.Id);

            Assert.That(result.Count, Is.EqualTo(group.GroupPermissions.Count()));
        }

        [Test]
        public async Task AddGroupPermission_ShouldAddPermissionToGroup()
        {
            var group = _fixture.Create<Group>();
            var permission = _fixture.Create<Permission>();

            _sut.Context.Permission.Add(permission);
            _sut.Context.Group.Add(group);
            await _sut.Context.SaveChangesAsync();

            await _sut.AddGroupPermission(group.Id, permission.Id);

            Assert.That(_sut.Context.GroupPermission.FirstOrDefault(x => x.GroupId == group.Id && x.PermissionId == permission.Id), Is.Not.EqualTo(null));
        }

        [Test]
        public async Task RemoveGroupPermissions_ExistingGroupPermission_ShouldRemovePermissionToGroup()
        {
            var group = _fixture.Create<Group>();
            var permission = _fixture.Create<Permission>();
            var groupPermission = new GroupPermission
            {
                GroupId = group.Id,
                PermissionId = permission.Id
            };

            _sut.Context.Permission.Add(permission);
            _sut.Context.Group.Add(group);
            _sut.Context.GroupPermission.Add(groupPermission);
            await _sut.Context.SaveChangesAsync();


            await _sut.RemoveGroupPermission(group.Id, permission.Id);

            Assert.That(_sut.Context.GroupPermission.FirstOrDefault(x => x.GroupId == group.Id && x.PermissionId == permission.Id), Is.EqualTo(null));
        }

        [Test]
        public async Task RemoveGroupPermissions_InvalidGroupPermission_ShouldThrowNotFoundException()
        {
            var group = _fixture.Create<Group>();
            var permission = _fixture.Create<Permission>();

            _sut.Context.Permission.Add(permission);
            _sut.Context.Group.Add(group);
            await _sut.Context.SaveChangesAsync();

            Assert.ThrowsAsync<NotFoundException>(() => _sut.RemoveGroupPermission(group.Id, permission.Id));            
        }
    }
}
