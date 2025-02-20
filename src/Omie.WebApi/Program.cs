using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Omie.IoC;
namespace Omie.WebApi;

public static class Program
{
    private const string ClientidJwtSettginConst = "OmieJwt:Audience";
    private const string AuthUrlJwtSettingConst = "OmieJwt:Issuer";
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
            
            // Define the BearerAuth scheme that's in use (JWT)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"<h3  class='text-info'>JWT Authorization header usando o esquema Bearer.</h3> <br>
                      <br> Digite 'Bearer' <strong>[espaço]</strong> e, em seguida, seu token no campo de texto abaixo.  
                      <br>Exemplo: <div class='opblock-summary-method'>Bearer 12345abcdef</div> 
                      <strong>Obtenha seu token na API:<strong> <a href='http://localhost:5030'>OmieAuthentication.WebApi</a>",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
        
        // Faking JWT Authentication using API Key
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            // Lets apply token validation parameters as well (even though I'm faking it with an API key)
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
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
