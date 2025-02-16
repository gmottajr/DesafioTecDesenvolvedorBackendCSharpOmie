using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Omie.DAL.Data;

public class OmieDbContextFactory : IDesignTimeDbContextFactory<DbContextOmie>
    {
        public DbContextOmie CreateDbContext(string[] args)
        {
            
            // Get the directory of the current executing assembly (Omie.DAL project directory)
            var currentProjectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            // Use DirectoryInfo to get the parent directory (one level up to the project root)
            var projectDirectory = new DirectoryInfo(currentProjectPath).Parent?.Parent?.Parent?.Parent?.FullName; // Go up twice to the solution folder
            
            // Assuming the WebApi project is at the same level as the DAL project
            var webApiProjectPath = Path.Combine(projectDirectory, "Omie.WebApi");
            
            // Set up configuration to read the appsettings.json from the WebApi project
            var configuration = new ConfigurationBuilder()
                .SetBasePath(webApiProjectPath)  // Point to WebApi project directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Add the config file
                .Build();


            // Retrieve the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Set up options for DbContext
            var optionsBuilder = new DbContextOptionsBuilder<DbContextOmie>();
            optionsBuilder.UseSqlServer(connectionString);

            // Return the DbContext instance with the options
            return new DbContextOmie(optionsBuilder.Options);
        }
    }
