using IT_Talent_Program.Entities;
using Microsoft.EntityFrameworkCore;

namespace IT_Talent_Program.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
