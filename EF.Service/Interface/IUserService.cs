using EF.Repository.Model;

namespace EF.Service.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetProfileAsync();
    }
}
