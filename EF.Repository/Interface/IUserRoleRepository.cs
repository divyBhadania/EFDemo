using EF.Repository.Model;

namespace EF.Repository.Interface
{
    public interface IUserRoleRepository
    {
        Task<List<string>> GetRolesByIdAsync(int id);
    }
}
