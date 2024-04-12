using IT_Talent_Program.Data;
using IT_Talent_Program.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IT_Talent_Program.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("Sql"));
            });
            services.AddScoped<ITokenGen, TokenGen>();
            services.AddScoped<IUserRepository, UserRepository>();
     

            return services;
        }
    }
}
