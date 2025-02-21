using Omie.Application;
using Omie.Application.Services;
using Omie.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Omie.Application.Authenticating;
using Omie.Application.Models; // Ensure this namespace contains JwtConfigDto
using Omie.Application.Services.Authenticating;
using Omie.Common.Abstractions.Core.Logging;
using Omie.Common.AOP.Logging;
using Omie.Common.Core.Logging;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Omie.IoC;

public static class IoCRegisterDiServicesHandler
{
// Register In-Memory Database
    public static IServiceCollection DbContextDiRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContextOmie>(options =>
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString)
                                .LogTo(Console.WriteLine, LogLevel.Information);
        });

        return services;
    }

    // Register Application Services
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClienteAppService, ClienteAppService>();
        services.AddScoped<IProdutoAppService, ProdutoAppService>();
        services.AddScoped<IVendaAppService, VendaAppService>();
        services.AddScoped<IAuthenticatingService, AuthenticatingService>();
        return services;
    }

    // Register Repositories
    public static IServiceCollection DataRepositoriesDiRegistration(this IServiceCollection services)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IVendaRepository, VendaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        return services;
    }

    // Register Configuration from appsettings.json and user-secrets.json
    public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var omieJwtSection = configuration.GetSection("OmieJwt");
        var jwtConfig = omieJwtSection.Get<JwtConfigDto>();
        //Console.WriteLine($"JWT Config: {Newtonsoft.Json.JsonConvert.SerializeObject(jwtConfig)}");
        
        services.Configure<JwtConfigDto>(configuration.GetSection("OmieJwt"));
        return services;
    }

    // Register Mapster
    public static IServiceCollection ConfigAddMapster(this IServiceCollection services)
    {
        services.AddMapster();
        return services;
    }

    // Register Logger Serilog
    public static IServiceCollection AddSerilogLogger(this IServiceCollection services, ConfigureHostBuilder hostBuilder)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        hostBuilder.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration) // Load from appsettings.json
                .ReadFrom.Services(services)                    // Access DI services (e.g., IHttpContextAccessor)
                .Enrich.FromLogContext()                        // Enables dynamic enrichment
                .Enrich.WithProperty("Environment", environmentName)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: @$"logs_{environmentName}_{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}/log.txt",
                    rollingInterval: Serilog.RollingInterval.Day,       // Daily log files
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}");

            // Minimum log levels (overridden by appsettings.json if present)
            configuration.MinimumLevel.Information();
            configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning); // Reduce noise
        });

        return services;
    }
    
    public static IServiceCollection RegisterLogAddapter(this IServiceCollection services)
    {
        services.AddHttpContextAccessor(); // For correlationId, userId
        services.AddScoped<ILoggerOmie, LoggerAdapter>();
        services.AddScoped<ILogPersister, FileLogPersister>(); 
        return services;
    }

    /// Verifies whether or not the database is created and applies any pending migrations.
    public static IServiceCollection EnsureDatabaseCreated(this IServiceCollection services)
    {
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DbContextOmie>();
            try
            {
                if (!dbContext.Database.CanConnect() || dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetService<ILoggerOmie>();
                logger?.LogError("Failed to check or migrate database on startup.", ex);
                throw; // Or handle gracefully
            }
        }

        return services;
    }
}
