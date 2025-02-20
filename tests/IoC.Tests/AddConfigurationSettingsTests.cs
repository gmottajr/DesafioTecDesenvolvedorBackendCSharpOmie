using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Omie.Application.Models;
using Omie.IoC;
using Xunit;

namespace IoC.Tests;

public class AddConfigurationSettingsTests
{
    [Fact]
    public void AddConfigurationSettings_ValidConfig_ReturnsServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();
        var configMock = new Mock<IConfiguration>();
        var configSectionMock = new Mock<IConfigurationSection>();
        configMock.Setup(x => x.GetSection("OmieJwt")).Returns(configSectionMock.Object);

        // Act
        var result = IoCRegisterDiServicesHandler.AddConfigurationSettings(services, configMock.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(services, s => s.ServiceType == typeof(IConfigureOptions<JwtConfigDto>));
    }

    [Fact]
    public void AddConfigurationSettings_NullConfigSection_StillRegisters()
    {
        // Arrange
        var services = new ServiceCollection();
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(x => x.GetSection("OmieJwt")).Returns((IConfigurationSection)null);

        // Act
        var result = IoCRegisterDiServicesHandler.AddConfigurationSettings(services, configMock.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(services, s => s.ServiceType == typeof(IConfigureOptions<JwtConfigDto>));
    }
}
