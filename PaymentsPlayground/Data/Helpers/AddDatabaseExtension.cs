using Microsoft.EntityFrameworkCore;

namespace PaymentsPlayground.Data.Helpers
{
    public static class AddDatabaseExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(
                    config["ConnectionStrings:SqlServerConnection"]));

            services.AddIdentityServices();

            return services;
        }
    }
}
