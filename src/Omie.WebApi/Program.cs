using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Omie.IoC;
namespace Omie.WebApi;

public static class Program
{
    private const string ClientidJwtSettginConst = "OmieJwt:ClientId";
    private const string AuthUrlJwtSettingConst = "OmieJwt:AuthUrl";
    private const string ClientsecretJwtSettingConst = "OmieJwt:ClientSecret";
    private const string ApikeyJwtConst = "OmieJwt:ApiKey";
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
        builder.Services.ConfigAddMapster();

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
        
        // Faking JWT Authentication using API Key
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            // Use the Authorization header, and validate it like a JWT token (using API key in this case)
            
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // Get the "Bearer token" from the Authorization header (this could be your API key instead of a JWT token)
                    var apiKey = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                    // Check if the API key is valid
                    if (string.IsNullOrEmpty(apiKey) || apiKey != builder.Configuration[ApikeyJwtConst])
                    {
                        context.Fail("Unauthorized");
                    }

                    // Proceed with the request if API key is valid
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    // Check if the token matches the expected Issuer, Audience, and SigningKey from the config
                    var apiKey = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                    if (apiKey != builder.Configuration[ApikeyJwtConst])
                    {
                        context.Fail("Invalid API Key");
                    }

                    // Validate if the Issuer and Audience from the configuration match the expected values (even if we're using an API Key)
                    if (context.Principal?.FindFirst("iss")?.Value != builder.Configuration[AuthUrlJwtSettingConst] ||
                        context.Principal?.Claims.FirstOrDefault(c => c.Type == "aud")?.Value != builder.Configuration[ClientidJwtSettginConst])
                    {
                        context.Fail("Invalid Issuer or Audience");
                    }

                    return Task.CompletedTask;
                }
            };

            // Lets apply token validation parameters as well (even though I'm faking it with an API key)
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                // I'm using this configuration just to proove familiarity with this setup
                ValidIssuer = builder.Configuration[AuthUrlJwtSettingConst], // Validate the Issuer
                ValidAudience = builder.Configuration[ClientidJwtSettginConst], // Validate the Audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[ClientsecretJwtSettingConst])) // Validate Signing Key
            };
        });

        builder.Services.AddAuthorization();
        
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
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
