using Microsoft.Extensions.DependencyInjection;
using Omie.IoC;

namespace IoC.Tests;

public class ConfigAddMapsterTests
{
    [Fact]
    public void ConfigAddMapster_ValidRegistration_ReturnsServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = IoCRegisterDiServicesHandler.ConfigAddMapster(services);

        // Assert
        Assert.NotNull(result);
        // Note: Exact Mapster service registrations might vary based on Mapster version
        Assert.NotEmpty(services);
    }
}
