using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Extensions;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static void AddDbContext<TContext>(this IServiceCollection services,
        IConfiguration configuration, string databaseKey) where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            var dbConnectionString =
                configuration
                    .GetSection("ConnectionStrings")
                    .GetSection(databaseKey)
                    .Value ?? string.Empty;

            ArgumentNullException.ThrowIfNull(dbConnectionString);

            options.UseNpgsql(dbConnectionString);
        });
    }

}