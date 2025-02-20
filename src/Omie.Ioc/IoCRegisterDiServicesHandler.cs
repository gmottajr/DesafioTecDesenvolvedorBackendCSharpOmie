using Omie.Application;
using Omie.Application.Services;
using Omie.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mapster;
using MapsterMapper;
using Omie.Application.Authenticating;
using Omie.Application.Models; // Ensure this namespace contains JwtConfigDto
using Omie.Application.Services.Authenticating;
using Omie.Application.Models;

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
}
