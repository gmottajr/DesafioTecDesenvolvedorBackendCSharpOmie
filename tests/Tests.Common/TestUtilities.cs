using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.IO;
using System.Reflection;

namespace Tests.Common;

public static class TestUtilities
{
    public static IConfigurationRoot LoadConfiguration() 
    {
        var gotPath = Assembly.GetExecutingAssembly().Location;
        var assemblyLocation = Path.GetDirectoryName(gotPath);
        if (string.IsNullOrEmpty(assemblyLocation))
        {
            throw new InvalidOperationException("Could not determine the assembly location.");
        }

        return new ConfigurationBuilder()
            .SetBasePath(assemblyLocation)  // Set base path dynamically based on the assembly of the type `T`
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
            .Build();
    }
    
}
