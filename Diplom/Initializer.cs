using Diplom.Models.Account;
using Diplom.Services.Implementations.Admin;
using Diplom.Services.Implementations.Repositories;
using Diplom.Services.Implementations;
using Diplom.Services.Interfaces;

namespace Diplom
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
