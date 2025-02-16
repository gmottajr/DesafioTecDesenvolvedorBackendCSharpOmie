using System.Reflection;
using Microsoft.OpenApi.Models;
using Omie.IoC;
namespace Omie.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load configuration from appsettings.json and User Secrets
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<VendaController>()
            .AddEnvironmentVariables();

        builder.Services.DbContextDiRegistration(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.DataRepositoriesDiRegistration();
        builder.Services.AddConfigurationSettings(builder.Configuration);

        // Add services to the container
        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "Omie Vendas - WebApi", 
                Version = "v1",
                Description = "Omie Vendas - WebApi",
                Contact = new OpenApiContact {
                    Name = "Gerson Jr",
                    Email = "contact@Omie.com",
                    Url = new Uri("https://www.Omie.com")
                }
                });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Omie Vendas - WebApi");
                c.RoutePrefix = string.Empty;
            });
        }

        // Configure the HTTP request pipeline
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
