using Microsoft.Extensions.DependencyInjection;
using Omie.DAL;
using Omie.IoC;

namespace IoC.Tests;

public class DataRepositoriesDiRegistrationTests
{
    [Fact]
    public void DataRepositoriesDiRegistration_ValidRegistration_ReturnsServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = IoCRegisterDiServicesHandler.DataRepositoriesDiRegistration(services);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, services.Count); // Expecting 3 scoped services
        Assert.Contains(services, s => s.ServiceType == typeof(IClienteRepository) && s.ImplementationType == typeof(ClienteRepository));
        Assert.Contains(services, s => s.ServiceType == typeof(IVendaRepository) && s.ImplementationType == typeof(VendaRepository));
        Assert.Contains(services, s => s.ServiceType == typeof(IProdutoRepository) && s.ImplementationType == typeof(ProdutoRepository));
    }
}
