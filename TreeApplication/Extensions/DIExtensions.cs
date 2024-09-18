using Serilog.Events;
using Serilog;
using TreeApplication.Services.Abstraction;
using TreeApplication.Services.Concrete;

namespace TreeApplication.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITreeService, TreeService>();

            return services;
        }

        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            return services;
        }
    }
}