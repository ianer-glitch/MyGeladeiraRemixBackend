using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        services.AddDbContext<TContext>(options =>
        {
            var dbConnectionString =
                configuration
                    .GetSection("ConnectionStrings")
                    .GetSection(databaseKey)
                    .Value ?? string.Empty;



            options.UseNpgsql(dbConnectionString);
        });

        return services;

    }

    public static IServiceCollection ConfigureJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {

        var jwtConfiguration = configuration.GetSection("JwtConfiguration");
        var issuer = jwtConfiguration.GetSection("Authority").Value;
        var audience = jwtConfiguration.GetSection("Audience").Value;
        var currentAudience = jwtConfiguration.GetSection("CurrentAudience").Value;
        var audienceToValidate = jwtConfiguration.GetSection("Audience").Value.Split(';')
            .FirstOrDefault(f => f.Contains(currentAudience));

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
                        ValidAudience = audienceToValidate,
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



}