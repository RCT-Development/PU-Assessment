using Moq;
using PU.Core.DTO.Response;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;

namespace PU.Services.UnitTests.Services
{
    [TestFixture]
    public class GroupServiceTests
    {
        private GroupService _sut;
        private Mock<IGroupRepository> _groupRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _sut = new GroupService(_groupRepositoryMock.Object);
        }

        [Test]
        public async Task CreateGroup_ValidInput_CallsRepositoryCreateAsync()
        {
            var group = new Group();

            await _sut.CreateAsync(group);

            _groupRepositoryMock.Verify(repo => repo.CreateAsync(group), Times.Once);
        }

        [Test]
        public async Task DeleteGroup_ExistingGroup_CallsRepositoryDeleteAsync()
        {
            var groupId = Guid.NewGuid();
            var existingGroup = new Group();
            _groupRepositoryMock.Setup(repo => repo.GetAsync(groupId)).ReturnsAsync(existingGroup);

            await _sut.DeleteAsync(groupId);

            _groupRepositoryMock.Verify(repo => repo.DeleteAsync(existingGroup), Times.Once);
        }

        [Test]
        public void DeleteGroup_NonExistingGroup_ThrowsNotFoundException()
        {
            var groupId = Guid.NewGuid();
            _groupRepositoryMock.Setup(repo => repo.GetAsync(groupId)).ReturnsAsync((Group)null);

            Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteAsync(groupId));
        }

        [Test]
        public async Task GetGroup_ReturnsGroup()
        {
            var groupId = Guid.NewGuid();
            var group = new Group { Id = groupId };
            _groupRepositoryMock.Setup(repo => repo.GetAsync(groupId)).ReturnsAsync(group);

            var result = await _sut.GetAsync(groupId);

            Assert.That(result.Id, Is.EqualTo(groupId));
        }

        [Test]
        public async Task GetGroups_ReturnsListOfGroups()
        {
            var groups = new List<Group> { new Group(), new Group() };
            _groupRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(groups);

            var result = await _sut.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(groups.Count));
        }

        [Test]
        public async Task GetUserCountPerGroup_ReturnsUserCounts()
        {
            var userCounts = new List<GroupUserCount>
            {
                new GroupUserCount { GroupName = string.Empty, UserCount = 3 },
                new GroupUserCount { GroupName = string.Empty, UserCount = 5 },
            };
            _groupRepositoryMock.Setup(repo => repo.GetUserCountPerGroup()).ReturnsAsync(userCounts);

            var result = await _sut.GetUserCountPerGroup();

            Assert.That(result.Count(), Is.EqualTo(userCounts.Count));
        }

        [Test]
        public async Task UpdateGroup_CallsRepositoryUpdateAsync()
        {
            var group = new Group();

            await _sut.UpdateGroup(group);

            _groupRepositoryMock.Verify(repo => repo.UpdateAsync(group), Times.Once);
        }

        [Test]
        public async Task GetGroupUsers_ReturnsUsers()
        {
            var groupId = Guid.NewGuid();
            var users = new List<User> { new User(), new User() };
            _groupRepositoryMock.Setup(repo => repo.GetGroupUsers(groupId)).ReturnsAsync(users);

            var result = await _sut.GetGroupUsersAsync(groupId);

            Assert.That(result.Count(), Is.EqualTo(users.Count));
        }

        [Test]
        public async Task AddGroupUser_CallsRepositoryAddGroupUser()
        {
            var groupId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            await _sut.AddGroupUserAsync(groupId, userId);

            _groupRepositoryMock.Verify(repo => repo.AddGroupUser(groupId, userId), Times.Once);
        }

        [Test]
        public async Task RemoveGroupUser_CallsRepositoryRemoveGroupUser()
        {
            var groupId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            await _sut.RemoveGroupUserAsync(groupId, userId);

            _groupRepositoryMock.Verify(repo => repo.RemoveGroupUser(groupId, userId), Times.Once);
        }

        [Test]
        public async Task GetGroupPermissions_ReturnsPermissions()
        {
            // Arrange
            var groupId = Guid.NewGuid();
            var permissions = new List<Permission> { new Permission(), new Permission() };
            _groupRepositoryMock.Setup(repo => repo.GetGroupPermissions(groupId)).ReturnsAsync(permissions);

            // Act
            var result = await _sut.GetGroupPermissionsAsync(groupId);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(permissions.Count));
        }

        [Test]
        public async Task AddGroupPermission_CallsRepositoryAddGroupPermission()
        {
            var groupId = Guid.NewGuid();
            var permissionId = Guid.NewGuid();

            await _sut.AddGroupPermissionAsync(groupId, permissionId);

            _groupRepositoryMock.Verify(repo => repo.AddGroupPermission(groupId, permissionId), Times.Once);
        }

        [Test]
        public async Task RemoveGroupPermission_CallsRepositoryRemoveGroupPermission()
        {
            var groupId = Guid.NewGuid();
            var permissionId = Guid.NewGuid();

            await _sut.RemoveGroupPermissionAsync(groupId, permissionId);

            _groupRepositoryMock.Verify(repo => repo.RemoveGroupPermission(groupId, permissionId), Times.Once);
        }

    }
}
