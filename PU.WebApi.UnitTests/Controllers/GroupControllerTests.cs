using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Moq;
using PU.Core.DTO.Request;
using PU.Core.Models;
using PU.Core.Services.Contracts;
using PU.WebApi.Controllers;

namespace PU.WebApi.UnitTests.Controllers
{
    [TestFixture]
    public class GroupControllerTests
    {
        private GroupController _sut;
        private Mock<IGroupService> _groupServiceMock;
        private Mock<IMapper> _mapperMock;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _groupServiceMock = new Mock<IGroupService>();
            _mapperMock = new Mock<IMapper>();
            _sut = new GroupController(_groupServiceMock.Object, _mapperMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllGroups()
        {
            var expectedGroups = _fixture.CreateMany<Group>();
            _groupServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedGroups);

            var result = await _sut.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedGroups));
        }

        [Test]
        public async Task GetAsync_ReturnsGroupById()
        {

            var groupId = Guid.NewGuid();
            var expectedGroup = _fixture.Build<Group>().With(x => x.Id, groupId).Create();
            _groupServiceMock.Setup(service => service.GetAsync(groupId)).ReturnsAsync(expectedGroup);

            var result = await _sut.GetAsync(groupId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(groupId));
        }

        [Test]
        public async Task CreateAsync_ReturnsOkResult()
        {
            var groupRequest = _fixture.Create<GroupRequest>();
            var group = _fixture.Create<Group>();
            _mapperMock.Setup(mapper => mapper.Map<Group>(groupRequest)).Returns(group);

            var result = await _sut.CreateAsync(groupRequest);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task UpdateAsync_ReturnsOkResult()
        {
            var groupRequest = _fixture.Create<GroupRequest>();
            var group = _fixture.Create<Group>();
            var groupId = Guid.NewGuid();
            _mapperMock.Setup(mapper => mapper.Map<Group>(groupRequest)).Returns(group);

            var result = await _sut.UpdateAsync(groupId, groupRequest);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task DeleteAsync_ReturnsOkResult()
        {
            var groupId = Guid.NewGuid();

            var result = await _sut.DeleteAsync(groupId);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task GetGroupUsersAsync_ReturnsGroupUsers()
        {
            var groupId = Guid.NewGuid();
            var expectedUsers = _fixture.CreateMany<User>();

            _groupServiceMock.Setup(service => service.GetGroupUsersAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUsers);

            var result = await _sut.GetGroupUsersAsync(groupId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expectedUsers.Count()));
        }

        [Test]
        public async Task GetGroupPermissionsAsync_ReturnsGroupPermissions()
        {
            var groupId = Guid.NewGuid();
            var expectedPermissions = _fixture.CreateMany<Permission>();

            _groupServiceMock.Setup(service => service.GetGroupPermissionsAsync(It.IsAny<Guid>())).ReturnsAsync(expectedPermissions);

            var result = await _sut.GetGroupPermissionsAsync(groupId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expectedPermissions.Count()));
        }

        [Test]
        public async Task AddGroupUserAsync_ReturnsOkResult()
        {
            var groupId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var result = await _sut.AddGroupUserAsync(groupId, userId);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task RemoveGroupUserAsync_ReturnsOkResult()
        {
            var groupId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var result = await _sut.RemoveGroupUserAsync(groupId, userId);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task AddGroupPermissionAsync_ReturnsOkResult()
        {
            var groupId = Guid.NewGuid();
            var permissionId = Guid.NewGuid();

            var result = await _sut.AddGroupPermissionAsync(groupId, permissionId);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task RemoveGroupPermissionAsync_ReturnsOkResult()
        {
            var groupId = Guid.NewGuid();
            var permissionId = Guid.NewGuid();

            var result = await _sut.RemoveGroupPermissionAsync(groupId, permissionId);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }
    }
}
