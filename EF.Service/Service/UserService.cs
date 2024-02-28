using EF.Repository.Interface;
using EF.Repository.Model;
using EF.Service.Interface;

namespace EF.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await _userRepository.GetByIdAsync(id) ?? new User();
            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public async Task<User> GetProfileAsync()
        {
            try
            {
                return await _userRepository.GetByIdAsync(1) ?? new User();
            }
            catch (Exception ex)
            {
                return new User();
            }
        }
    }
}
