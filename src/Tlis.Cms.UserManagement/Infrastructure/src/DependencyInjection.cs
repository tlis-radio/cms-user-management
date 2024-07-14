using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;
using Tlis.Cms.UserManagement.Infrastructure.HttpServices;
using Tlis.Cms.UserManagement.Infrastructure.HttpServices.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Persistence;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<CmsServicesConfiguration>()
            .Bind(configuration.GetSection("CmsServices"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext(configuration);
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddAuthProviderManagementService(configuration);
        services
            .AddHttpClient<IImageManagementHttpService, ImageManagementHttpService>()
            .AddStandardResilienceHandler();
    }

    public static void AddAuthProviderManagementService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<Auth0Configuration>()
            .Bind(configuration.GetSection("Auth0"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient<ITokenProviderService, TokenProviderService>();
        services.AddScoped<IAuthProviderManagementService, AuthProviderManagementService>();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IUserManagementDbContext, UserManagementDbContext>(options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, UserManagementDbContext.SCHEMA))
                    .UseSnakeCaseNamingConvention();
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Singleton);
    }
}