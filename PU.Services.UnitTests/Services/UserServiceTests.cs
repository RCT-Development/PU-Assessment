using Moq;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;

namespace PU.Services.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _sut;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _sut = new UserService(_userRepositoryMock.Object);
        }

        [Test]
        public async Task CreateUser_ValidInput_CallsRepositoryCreateAsync()
        {
            var user = new User();

            await _sut.CreateUser(user);

            _userRepositoryMock.Verify(repo => repo.CreateAsync(user), Times.Once);
        }

        [Test]
        public async Task DeleteUser_ExistingUser_CallsRepositoryDeleteAsync()
        {
            var userId = Guid.NewGuid();
            var existingUser = new User();
            _userRepositoryMock.Setup(repo => repo.GetAsync(userId)).ReturnsAsync(existingUser);

            await _sut.DeleteUser(userId);

            _userRepositoryMock.Verify(repo => repo.DeleteAsync(existingUser), Times.Once);
        }

        [Test]
        public void DeleteUser_NonExistingUser_ThrowsNotFoundException()
        {
            var UserId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.GetAsync(UserId)).ReturnsAsync((User)null);

            Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteUser(UserId));
        }

        [Test]
        public async Task GetUser_ReturnsUser()
        {
            var UserId = Guid.NewGuid();
            var User = new User { Id = UserId };
            _userRepositoryMock.Setup(repo => repo.GetAsync(UserId)).ReturnsAsync(User);

            var result = await _sut.GetUser(UserId);

            Assert.That(result.Id, Is.EqualTo(UserId));
        }

        [Test]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            var users = new List<User> { new User(), new User() };
            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var result = await _sut.GetUsers();

            Assert.That(result.Count(), Is.EqualTo(users.Count));
        }

        [Test]
        public async Task UpdateUser_CallsRepositoryUpdateAsync()
        {
            var user = new User();

            await _sut.UpdateUser(user);

            _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
        }
    }
}
