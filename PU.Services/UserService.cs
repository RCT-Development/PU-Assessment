using PU.Core.DTO.Request;
using PU.Core.Exceptions;
using PU.Core.Models;
using PU.Core.Repositories;
using PU.Core.Services.Contracts;

namespace PU.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task CreateUser(User model)
        {
            await _userRepository.CreateAsync(model);
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User: {id} does not exist");
            }
            await _userRepository.DeleteAsync(user);
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task UpdateUser(User model)
        {
            await _userRepository.UpdateAsync(model);
        }
    }
}
