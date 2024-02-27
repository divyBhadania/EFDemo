using EF.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace EF.Repository
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "Admin", IsActive = true },
                new Role() { Id = 2, Name = "Member", IsActive = true });

            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Name = "Admin", Email = "admin@gmail.com", Password = "admin@123", CreatedOn = new DateTime(2024, 02, 07), IsActive = true },
                new User() { Id = 2, Name = "member", Email = "member@gmail.com", Password = "member@123", CreatedOn = new DateTime(2024, 02, 07), IsActive = true });
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole() { Id = 1, UserId = 1, RoleId = 1, IsActive = true },
                new UserRole() { Id = 2, UserId = 1, RoleId = 2, IsActive = true },
                new UserRole() { Id = 3, UserId = 2, RoleId = 2, IsActive = true });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
