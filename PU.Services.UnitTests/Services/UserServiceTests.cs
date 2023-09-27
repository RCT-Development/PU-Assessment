using AutoFixture;
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
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _sut = new UserService(_userRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task CreateUser_ValidInput_CallsRepositoryCreateAsync()
        {
            var user = _fixture.Create<User>();

            await _sut.CreateAsync(user);

            _userRepositoryMock.Verify(repo => repo.CreateAsync(user), Times.Once);
        }

        [Test]
        public async Task DeleteUser_ExistingUser_CallsRepositoryDeleteAsync()
        {
            var userId = Guid.NewGuid();
            var existingUser = _fixture.Create<User>();
            _userRepositoryMock.Setup(repo => repo.GetAsync(userId)).ReturnsAsync(existingUser);

            await _sut.DeleteAsync(userId);

            _userRepositoryMock.Verify(repo => repo.DeleteAsync(existingUser), Times.Once);
        }

        [Test]
        public void DeleteUser_NonExistingUser_ThrowsNotFoundException()
        {
            var UserId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.GetAsync(UserId)).ReturnsAsync((User)null);

            Assert.ThrowsAsync<NotFoundException>(() => _sut.DeleteAsync(UserId));
        }

        [Test]
        public async Task GetUser_ReturnsUser()
        {
            var UserId = Guid.NewGuid();
            var User = _fixture.Build<User>().With(x => x.Id, UserId).Create();
            _userRepositoryMock.Setup(repo => repo.GetAsync(UserId)).ReturnsAsync(User);

            var result = await _sut.GetAsync(UserId);

            Assert.That(result.Id, Is.EqualTo(UserId));
        }

        [Test]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            var users = _fixture.CreateMany<User>();
            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var result = await _sut.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(users.Count()));
        }

        [Test]
        public async Task UpdateUser_CallsRepositoryUpdateAsync()
        {
            var user = _fixture.Create<User>();

            await _sut.UpdateAsync(user);

            _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
        }
    }
}
