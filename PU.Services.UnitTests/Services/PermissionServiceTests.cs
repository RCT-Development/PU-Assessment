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

        [SetUp]
        public void Setup()
        {
            _permissionRepositoryMock = new Mock<IPermissionRepository>();
            _sut = new PermissionService(_permissionRepositoryMock.Object);
        }

        [Test]
        public async Task CreatePermission_ValidInput_CallsRepositoryCreateAsync()
        {
            var permission = new Permission();

            await _sut.CreateAsync(permission);

            _permissionRepositoryMock.Verify(repo => repo.CreateAsync(permission), Times.Once);
        }

        [Test]
        public async Task DeletePermission_ExistingPermission_CallsRepositoryDeleteAsync()
        {
            var permissionId = Guid.NewGuid();
            var existingPermission = new Permission();
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
            var Permission = new Permission { Id = PermissionId };
            _permissionRepositoryMock.Setup(repo => repo.GetAsync(PermissionId)).ReturnsAsync(Permission);

            var result = await _sut.GetAsync(PermissionId);

            Assert.That(result.Id, Is.EqualTo(PermissionId));
        }

        [Test]
        public async Task GetPermissions_ReturnsListOfPermissions()
        {
            var permissions = new List<Permission> { new Permission(), new Permission() };
            _permissionRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(permissions);

            var result = await _sut.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(permissions.Count));
        }

        [Test]
        public async Task UpdatePermission_CallsRepositoryUpdateAsync()
        {
            var permission = new Permission();

            await _sut.UpdateAsync(permission);

            _permissionRepositoryMock.Verify(repo => repo.UpdateAsync(permission), Times.Once);
        }
    }
}
