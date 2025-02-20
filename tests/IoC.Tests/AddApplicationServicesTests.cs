using Microsoft.Extensions.DependencyInjection;
using Omie.Application;
using Omie.Application.Authenticating;
using Omie.Application.Services;
using Omie.Application.Services.Authenticating;
using Omie.IoC;

namespace IoC.Tests;

public class AddApplicationServicesTests
{
[Fact]
        public void AddApplicationServices_ValidRegistration_ReturnsServiceCollection()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var result = IoCRegisterDiServicesHandler.AddApplicationServices(services);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, services.Count); // Expecting 4 scoped services
            Assert.Contains(services, s => s.ServiceType == typeof(IClienteAppService) && s.ImplementationType == typeof(ClienteAppService));
            Assert.Contains(services, s => s.ServiceType == typeof(IProdutoAppService) && s.ImplementationType == typeof(ProdutoAppService));
            Assert.Contains(services, s => s.ServiceType == typeof(IVendaAppService) && s.ImplementationType == typeof(VendaAppService));
            Assert.Contains(services, s => s.ServiceType == typeof(IAuthenticatingService) && s.ImplementationType == typeof(AuthenticatingService));
        }
}
