using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TreeApplication.DAL.Repositories.Abstraction;
using TreeApplication.DAL.Repositories.Concrete;

namespace TreeApplication.DAL.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Db"));
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITreeRepository, TreeRepository>();
            services.AddScoped<IExceptionJournalRepository, ExceptionJournalRepository>();

            return services;
        }
    }
}
