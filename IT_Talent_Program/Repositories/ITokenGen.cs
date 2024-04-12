using IT_Talent_Program.Entities;

namespace IT_Talent_Program.Repositories
{
    public interface ITokenGen
    {
        string GenerateToken(User user);
    }
}
