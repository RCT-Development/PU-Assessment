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
    public class UserControllerTests
    {
        private UserController _sut;
        private Mock<IUserService> _userServiceMock;
        private Mock<IMapper> _mapperMock;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _sut = new UserController(_userServiceMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            var expectedUsers = _fixture.CreateMany<User>();
            _userServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedUsers);

            var result = await _sut.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedUsers));
        }


        [Test]
        public async Task GetAsync_ReturnsUserById()
        {

            var userId = Guid.NewGuid();
            var expectedUser = _fixture.Build<User>().With(x => x.Id, userId).Create();
            _userServiceMock.Setup(service => service.GetAsync(userId)).ReturnsAsync(expectedUser);

            var result = await _sut.GetAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
        }

        [Test]
        public async Task CreateAsync_ReturnsOkResult()
        {
            var userRequest = _fixture.Create<UserRequest>();
            var user = _fixture.Create<User>();
            _mapperMock.Setup(mapper => mapper.Map<User>(userRequest)).Returns(user);

            var result = await _sut.CreateAsync(userRequest);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task UpdateAsync_ReturnsOkResult()
        {
            var userRequest = _fixture.Create<UserRequest>();
            var user = _fixture.Create<User>();
            var userId = Guid.NewGuid();
            _mapperMock.Setup(mapper => mapper.Map<User>(userRequest)).Returns(user);

            var result = await _sut.UpdateAsync(userId, userRequest);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task DeleteAsync_ReturnsOkResult()
        {
            var userId = Guid.NewGuid();

            var result = await _sut.DeleteAsync(userId);

            Assert.That(typeof(OkResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public async Task GetUserCountAsync_ReturnsNumberOfUsers()
        {
            var expectedUsers = _fixture.CreateMany<User>();
            _userServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedUsers);

            var result = await _sut.GetUserCountAsync();

            Assert.That(result, Is.EqualTo(expectedUsers.Count()));
        }
    }
}
