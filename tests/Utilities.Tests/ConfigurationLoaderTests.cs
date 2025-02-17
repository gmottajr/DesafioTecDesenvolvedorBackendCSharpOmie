using System;
using System.Data;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Omie.WebApi;
using Tests.Common;
using Microsoft.Extensions.Configuration;

namespace Utilities.Tests;

public class ConfigurationLoaderTests
{
    [Fact]
    public void LoadConfiguration_ShouldReturnValidConfiguration()
    {
        
        // Act: Call the method to load the configuration
        var configuration = TestUtilities.LoadConfiguration<VendaController>();

        // Assert: Validate the behavior of the method
        Assert.NotNull(configuration); // Ensure the configuration is not null
        Assert.NotEmpty(configuration.AsEnumerable()); // Ensure there are values loaded in the config (based on appsettings.json)
    }
    
    [Fact]
    public void LoadConfiguration_ShouldContainKeyInAppSettings()
    {
        // Arrange
        var testType = typeof(VendaController);  
        var keyToCheck = "Logging";        

        // Act: Call the method to load the configuration
        var configuration = TestUtilities.LoadConfiguration<VendaController>();

        // Assert: Check if the key exists in the configuration
        var keyValue = configuration[keyToCheck];

        // Assert that the key exists and the value is not null or empty
        Assert.NotNull(keyValue);  
        Assert.NotEmpty(keyValue); 
    }
}
