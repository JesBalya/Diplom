using Diplom.Models.Account;
using Diplom.Services.Implementations.Admin;
using Diplom.Services.Implementations.Repositories;
using Diplom.Services.Implementations;
using Diplom.Services.Interfaces;
using Diplom.Models.Entity;

namespace Diplom
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Consultation>, ConsultationRepository>();
            services.AddScoped<IBaseRepository<Subscription>, SubscriptionRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IConsultationService, ConsultationService>();
        }
    }
}
