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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = "admin",
                    Password = "admin123",
                    Name = "admin",
                    Gender = 2,
                    Admin = true,
                    CreatedBy = "system",
                    Birthday = new DateTime(2000,4,3)
                });
        }
    }
}
