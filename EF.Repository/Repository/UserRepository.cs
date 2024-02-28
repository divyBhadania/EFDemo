using EF.Repository.Interface;
using EF.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace EF.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EFDbContext _context;

        public UserRepository(EFDbContext context)
        {
            _context = context;
        }

        public async Task<int?> CheckUserAuthAsync(string email, string password)
        {
            return await _context.Users.Where(x => x.IsActive && x.Email.Equals(email) && x.Password.Equals(password)).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
            if (user != null)
            {
                user.IsActive = false;
                _context.Entry(user).Property("IsActive").IsModified = true;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().Where(x => x.IsActive).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public async Task<bool> InsertAsync(User user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            await _context.UserRoles.AddAsync(new UserRole() { UserId = user.Id, RoleId = 2, IsActive = true });
            var flag = await _context.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return flag;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Entry(user).Property("Name").IsModified = true;
            _context.Entry(user).Property("Email").IsModified = true;
            _context.Entry(user).Property("Password").IsModified = true;
            var flag = await _context.SaveChangesAsync() > 0;
            return flag;
        }
    }
}
