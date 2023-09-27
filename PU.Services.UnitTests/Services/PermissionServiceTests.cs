using AutoFixture;
using Moq;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;

namespace PU.Services.UnitTests.Services
{
    [TestFixture]
    public class PermissionServiceTests
    {
        private PermissionService _sut;
        private Mock<IPermissionRepository> _permissionRepositoryMock;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _permissionRepositoryMock = new Mock<IPermissionRepository>();
            _sut = new PermissionService(_permissionRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task CreatePermission_ValidInput_CallsRepositoryCreateAsync()
        {
            var permission = _fixture.Create<Permission>();

            await _sut.CreateAsync(permission);

            _permissionRepositoryMock.Verify(repo => repo.CreateAsync(permission), Times.Once);
        }

        [Test]
        public async Task DeletePermission_ExistingPermission_CallsRepositoryDeleteAsync()
        {
            var permissionId = Guid.NewGuid();
            var existingPermission = _fixture.Create<Permission>();
            _permissionRepositoryMock.Setup(repo => repo.GetAsync(permissionId)).ReturnsAsync(existingPermission);

            await _sut.DeleteAsync(permissionId);

            _permissionRepositoryMock.Verify(repo => repo.DeleteAsync(existingPermission), Times.Once);
        }

        [Test]
        public void DeletePermission_NonExistingPermission_ThrowsNotFoundException()
        {
            var PermissionId = Guid.NewGuid();
            _permissionRepositoryMock.Setup(repo => repo.GetAsync(PermissionId)).ReturnsAsync((Permission)null);

            Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteAsync(PermissionId));
        }

        [Test]
        public async Task GetPermission_ReturnsPermission()
        {
            var PermissionId = Guid.NewGuid();
            var Permission = _fixture.Build<Permission>().With(x => x.Id, PermissionId).Create();
            _permissionRepositoryMock.Setup(repo => repo.GetAsync(PermissionId)).ReturnsAsync(Permission);

            var result = await _sut.GetAsync(PermissionId);

            Assert.That(result.Id, Is.EqualTo(PermissionId));
        }

        [Test]
        public async Task GetPermissions_ReturnsListOfPermissions()
        {
            var permissions = _fixture.CreateMany<Permission>();
            _permissionRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(permissions);

            var result = await _sut.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(permissions.Count()));
        }

        [Test]
        public async Task UpdatePermission_CallsRepositoryUpdateAsync()
        {
            var permission = _fixture.Create<Permission>();

            await _sut.UpdateAsync(permission);

            _permissionRepositoryMock.Verify(repo => repo.UpdateAsync(permission), Times.Once);
        }
    }
}
