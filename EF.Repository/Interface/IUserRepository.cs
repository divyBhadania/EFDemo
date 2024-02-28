using EF.Repository.Model;

namespace EF.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<bool> InsertAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<int?> CheckUserAuthAsync(string email, string password);
    }
}
