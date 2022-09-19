using Microsoft.EntityFrameworkCore;
using webAPI.Entities;

namespace webAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<UserInfo> UserInfo { get; set; }
    }
}