using EF.Repository.Interface;
using EF.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace EF.Repository.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly EFDbContext _context;

        public UserRoleRepository(EFDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetRolesByIdAsync(int id)
        {
            return await _context.UserRoles.Include(x => x.Role)
                                           .Include(x => x.User)
                                           .Where(x => x.UserId == id && x.IsActive && x.User.IsActive && x.Role.IsActive)
                                           .Select(x => x.Role.Name)
                                           .ToListAsync();
        }
    }
}
