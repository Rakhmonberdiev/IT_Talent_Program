﻿using IT_Talent_Program.Entities;

namespace IT_Talent_Program.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByLogin(string login);
        Task Create(User user);
        Task Update(User user);
    }
}
