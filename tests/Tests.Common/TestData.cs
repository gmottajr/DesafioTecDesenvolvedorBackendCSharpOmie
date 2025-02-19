using Microsoft.EntityFrameworkCore;
using Tests.Common.Fixtures;
using Omie.Domain.Abstractions;
using Omie.Domain.Entities;
using Omie.Application.Models.Abstractions;
using Microsoft.Extensions.Configuration;
using Omie.DAL;

namespace Tests.Common.Data;

public static class TestData
{
    public const string DefaultTestConnectionStringAlias = "TestDbConnectionString";
    public static List<TEntity> GetEntities<TEntity, TDbContext>(DatabaseFixture<TEntity, TDbContext> fixture, int howMany = 1, bool ignoreNulldef = false)
        where TEntity : EntityBase
        where TDbContext : DbContext
    {
        var entities = fixture.GetEntities<TEntity>(howMany, ignoreNulldef);
        return entities;
    }
    public static TEntity GetEntity<TEntity, TDbContext>(DatabaseFixture<TEntity, TDbContext> fixture, bool ignoreNulldef = false)
        where TDbContext : DbContext
        where TEntity : EntityBase
    {
        var entity = fixture.GetEntityFilled<TEntity>();
        return entity;
    }
    // Example for specific entity like Cliente:
    public static Cliente GetCliente<TDbContext>(DatabaseFixture<Cliente, TDbContext> fixture, bool ignoreNulldef = false) 
        where TDbContext : DbContext
    {
        return GetEntity(fixture, ignoreNulldef);
    }
    
    
    public static Endereco GetEndereco<TDbContext>(DatabaseFixture<Endereco, TDbContext> fixture, bool ignoreNulldef = false) 
        where TDbContext : DbContext
    {
        return GetEntity(fixture, ignoreNulldef);
    }
    
    public static Produto GetProduto<TDbContext>(DatabaseFixture<Produto, TDbContext> fixture, bool ignoreNulldef = false) 
        where TDbContext : DbContext
    {
        return GetEntity(fixture, ignoreNulldef);
    }
    
    
    public static Venda GetVenda<TDbContext>(DatabaseFixture<Venda, TDbContext> fixture, bool ignoreNulldef = false) 
        where TDbContext : DbContext
    {
        return GetEntity(fixture, ignoreNulldef);
    }
    
    /// Static method to create a TDbContext instance with an in-memory database and apply migrations
    public  static TDbContext CreateInMemoryDbContext<TDbContext>() where TDbContext : DbContext
    {
        var options = new DbContextOptionsBuilder<TDbContext>()
            .UseInMemoryDatabase("InMemoryTestDb") 
            .Options;

        var context = (TDbContext)Activator.CreateInstance(typeof(TDbContext), options)!;
        
        return context;
    }

    public static TDbContext CreateSQlServerTestDbContext<TDbContext, TEntity>() 
        where TDbContext : DbContext
    {
        var config = TestUtilities.LoadConfiguration();
        var connectionString = config.GetConnectionString(DefaultTestConnectionStringAlias);
        connectionString = string.Format(connectionString ??
            throw new InvalidOperationException("Connection string not found in configuration file."), 
            typeof(TEntity).Name.Replace("Dto", string.Empty, StringComparison.CurrentCultureIgnoreCase));
        var options = new DbContextOptionsBuilder<TDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        var context = (TDbContext)Activator.CreateInstance(typeof(TDbContext), options)!;

        return context;
    }

    public static List<Cliente> GetClientes(DatabaseFixture<Cliente, DbContextOmie> fixture, int count, bool ignoreNulldef)
    {
        return GetEntities(fixture, count, ignoreNulldef);
    }

    public static IEnumerable<Venda> GetVendas(DatabaseFixture<Venda, DbContextOmie> fixture, int count)
    {
        return GetEntities(fixture, count);
    }
}
