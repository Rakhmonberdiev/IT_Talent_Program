using IT_Talent_Program.Entities;

namespace IT_Talent_Program.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByLogin(string login);
        Task<User> Create(User user);
    }
}
