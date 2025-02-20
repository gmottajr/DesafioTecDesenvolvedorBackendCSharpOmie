using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using Omie.DAL;
using Omie.IoC;
using FluentAssertions;

namespace IoC.Tests;

public class DbContextDiRegistrationTests
{
[Fact]
        public void DbContextDiRegistration_ValidConnectionString_SuccessfullyRegisters()
        {
            // Arrange
            var services = new ServiceCollection();
            var configMock = new Mock<IConfiguration>();
            var configSectionMock = new Mock<IConfigurationSection>();
            configSectionMock.Setup(x => x.Value).Returns("valid-connection-string");
            configMock.Setup(x => x.GetConnectionString("DefaultConnection")).Returns("valid-connection-string");

            // Act
            var result = IoCRegisterDiServicesHandler.DbContextDiRegistration(services, configMock.Object);

            // Assert
            result.Should().NotBeNull();
            services.Should().ContainSingle(s => s.ServiceType == typeof(DbContextOmie));
        }

        [Fact]
        public void DbContextDiRegistration_NullConnectionString_StillRegisters()
        {
            // Arrange
            var services = new ServiceCollection();
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x.GetConnectionString("DefaultConnection")).Returns((string)null);

            // Act
            var result = IoCRegisterDiServicesHandler.DbContextDiRegistration(services, configMock.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(services, s => s.ServiceType == typeof(DbContextOmie));
        }
}
