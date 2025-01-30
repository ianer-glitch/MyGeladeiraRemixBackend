using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Extensions;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services,
        IConfiguration configuration, string databaseKey) where TContext : DbContext
    {
        try
        {
            var dbConnectionString =
                configuration
                    .GetSection("ConnectionStrings")
                    .GetSection(databaseKey)
                    .Value ?? string.Empty;

            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(dbConnectionString);
            });

            return services;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

    }

    public static IServiceCollection ConfigureJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {

        var jwtConfiguration = configuration.GetSection("JwtConfiguration");
        var issuer = jwtConfiguration.GetSection("Authority").Value;
        var audiences = jwtConfiguration.GetSection("Audience").Value.Split(';');

        var key = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(jwtConfiguration.GetSection("SecurityKey").Value ??
                                throw new ArgumentException("Security key not Found")));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = issuer,
                        ValidAudiences = audiences,
                        IssuerSigningKey = key,
                    };

                    options.RequireHttpsMetadata = false;
                    options.IncludeErrorDetails = true;
                    
                });
        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,

            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        return services;
    }

    public static void ApplyMigrations<TContext>(this WebApplication app) where TContext : DbContext
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<TContext>();
            db.Database.Migrate();
        }
    }

}