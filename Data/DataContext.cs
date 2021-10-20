using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserService.Models;

namespace UserService.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}