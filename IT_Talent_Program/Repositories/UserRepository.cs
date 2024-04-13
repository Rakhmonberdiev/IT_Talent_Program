using IT_Talent_Program.Data;
using IT_Talent_Program.Entities;
using Microsoft.EntityFrameworkCore;

namespace IT_Talent_Program.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Create(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public async Task<List<User>> GetActiveUsers()
        {
            var activeUsers = await _db.Users
                .Where(u=>u.RevokedOn == null)
                .OrderBy(d=>d.CreatedOn)
                .ToListAsync();

            return activeUsers;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _db.Users.SingleOrDefaultAsync(x => x.Login == login);
        }

        public async Task<List<User>> GetUsersByAge(int age)
        {
            var min = DateTime.Today.AddYears(-age);
            var query = await _db.Users
                .Where(u => u.Birthday <= min)
                .ToListAsync();

 
            return query;
        }

        public async Task Update(User user)
        {
            User existingUser = _db.Users.Find(user.Id);
            _db.Users.Entry(existingUser).CurrentValues.SetValues(user);
            await _db.SaveChangesAsync();
        }
    }
}
