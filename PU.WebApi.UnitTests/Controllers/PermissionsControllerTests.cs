using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PU.Core.DTO.Request;
using PU.Core.Models;
using PU.Core.Services.Contracts;
using PU.WebApi.Controllers;

namespace PU.WebApi.UnitTests.Controllers
{
    [TestFixture]
    public class PermissionsControllerTests
    {
        private PermissionController _sut;
        private Mock<IPermissionService> _permissionServiceMock;
        private Mock<IMapper> _mapperMock;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _permissionServiceMock = new Mock<IPermissionService>();
            _mapperMock = new Mock<IMapper>();
            _sut = new PermissionController(_permissionServiceMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllPermissions()
        {
            var expectedPermissions = _fixture.CreateMany<Permission>();
            _permissionServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedPermissions);

            var result = await _sut.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedPermissions));
        }


        [Test]
        public async Task GetAsync_ReturnsPermissionById()
        {

            var permissionId = Guid.NewGuid();
            var expectedPermission = _fixture.Build<Permission>().With(x => x.Id, permissionId).Create();
            _permissionServiceMock.Setup(service => service.GetAsync(permissionId)).ReturnsAsync(expectedPermission);

            var result = await _sut.GetAsync(permissionId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(permissionId));
        }

        [Test]
        public async Task CreateAsync_ReturnsOkResult()
        {
            var permissionRequest = _fixture.Create<PermissionRequest>();
            var permission = _fixture.Create<Permission>();
            _mapperMock.Setup(mapper => mapper.Map<Permission>(permissionRequest)).Returns(permission);

            var result = await _sut.CreateAsync(permissionRequest);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task UpdateAsync_ReturnsOkResult()
        {
            var permissionRequest = _fixture.Create<PermissionRequest>();
            var permission = _fixture.Create<Permission>();
            var permissionId = Guid.NewGuid();
            _mapperMock.Setup(mapper => mapper.Map<Permission>(permissionRequest)).Returns(permission);

            var result = await _sut.UpdateAsync(permissionId, permissionRequest);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task DeleteAsync_ReturnsOkResult()
        {
            var permissionId = Guid.NewGuid();

            var result = await _sut.DeleteAsync(permissionId);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }
    }
}
