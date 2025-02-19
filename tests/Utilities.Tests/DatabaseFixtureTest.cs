using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Tests.Common.Fixtures;
using Omie.Domain.Abstractions;
using Tests.Common.Data;
using Omie.Domain.Entities;
using Omie.DAL;
using FluentAssertions;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Tests.Common;

public class DatabaseFixtureTest
{
    
    [Fact]
    public void DatabaseFixture_CreatesInMemoryDbContext()
    {
        var fixture = new DatabaseFixture<Cliente, DbContextOmie>();
        Assert.NotNull(fixture.Context);
        Assert.True(fixture.Context.Database.IsInMemory());
    }

    [Fact]
    public void DatabaseFixture_SwitchesToSqlServer()
    {
        var fixture = new DatabaseFixture<Cliente, DbContextOmie>();
        fixture.SetWorkWithSqlServer();
        Assert.False(fixture.Context.Database.IsInMemory());
    }

    [Fact]
    public void DatabaseFixture_SeedsData()
    {
        var fixture = new DatabaseFixture<Cliente, DbContextOmie>();
        List<Cliente> entities = TestData.GetClientes(fixture, 3, false);
        entities.ForEach(cliente => cliente = NormalizeCliente(cliente));        

        fixture.SeedData<long>(entities);
        fixture.Context.Set<Cliente>().Count().Should().Be(3);
        fixture.Context.Set<Cliente>().First().Should().BeEquivalentTo(entities[0]);
        fixture.Context.Set<Cliente>().Last().Should().BeEquivalentTo(entities[2]);
    }

    private static Cliente NormalizeCliente(Cliente cliente)
    {
        cliente.CPF = cliente.CPF.Substring(0, 11);
        cliente.Email = $"{cliente.Email.Substring(0, 10)}@teste{cliente.Email.Substring(10, 4)}.com";
        cliente.Telefone = cliente.Telefone.Substring(0, 11);
        cliente.Endereco.CEP = cliente.Endereco.CEP.Substring(0, 8);
        cliente.Endereco.Numero = cliente.Endereco.Numero.Substring(0, 2);
        cliente.Endereco.Complemento = cliente.Endereco.Complemento.Substring(0, 15);
        cliente.Endereco.Bairro = cliente.Endereco.Bairro.Substring(0, 10);
        cliente.Endereco.Cidade = cliente.Endereco.Cidade.Substring(0, 10);
        cliente.Endereco.Estado = cliente.Endereco.Estado.Substring(0, 2);
        return cliente;
    }

    [Fact]
    public void DatabaseFixture_SeedsDataSingle()
    {
        var fixture = new DatabaseFixture<Cliente, DbContextOmie>();
        Cliente entity = TestData.GetCliente(fixture, true);
        var cliente = NormalizeCliente(entity);
        fixture.SeedDataSingle(cliente);
        fixture.Context.Set<Cliente>().CountAsync().Result.Should().Be(1);
        fixture.Context.Set<Cliente>().FirstAsync().Result.Should().BeEquivalentTo(cliente);
    }

    [Fact]
    public void DatabaseFixture_GetsEntities()
    {
        var fixture = new DatabaseFixture<Venda, DbContextOmie>();
        var vendas = fixture.GetEntities<Venda>(5, false);

        vendas.Count.Should().Be(5);
        vendas.ForEach((Action<Venda>)(entity =>
        {
            entity.DataDaVenda.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(20));
            AssertionExtensions.Should(entity.Cliente).NotBeNull();
            AssertionExtensions.Should(entity.Cliente.Length).BeGreaterThan(2);
            entity.Itens.Count.Should().BeGreaterThan(0);
        }));

        vendas[0].Should().BeEquivalentTo(vendas[1]);
    }

    [Fact]
    public void DatabaseFixture_GetsEntityFilled()
    {
        var fixture = new DatabaseFixture<Venda, DbContextOmie>();
        Venda venda = fixture.GetVendas(1).FirstOrDefault();

        venda.Should().NotBeNull();
        //venda.DataDaVenda.Should().BeOfType(typeof(DateTime));
        venda.Should().BeOfType<Venda>();
        
        typeof(Venda).GetProperties().Should().BeEquivalentTo(venda.GetType().GetProperties());

        Cliente cliente = fixture.BuildCliente();
        cliente.Should().NotBeNull();
        //cliente.Nome.Should().BeOfType(typeof(Cliente.Nome));
        cliente.Should().BeOfType<Cliente>();
        cliente.GetType().IsAssignableFrom(typeof(Cliente)).Should().BeTrue();
        typeof(Cliente).GetProperties().Should().BeEquivalentTo(cliente.GetType().GetProperties());

        var produto = fixture.BuildProduto();
        produto.Should().NotBeNull();
        //produto.Nome.Should().BeOfType(typeof(string));
        produto.Should().BeOfType<Produto>();
        typeof(Produto).GetProperties().Should().BeEquivalentTo(produto.GetType().GetProperties());

    }

    [Fact]
    public void DatabaseFixture_Dispose()
    {
        var fixture = new DatabaseFixture<Cliente, DbContextOmie>();
        fixture.Dispose();

        Assert.Throws<ObjectDisposedException>(() => fixture.Context.Database.EnsureCreated());
    }

    [Fact]
    public void DatabaseFixture_SetupDataContextSqlServer()
    {
        // Arrange
        var fixture = new DatabaseFixture<Cliente, DbContextOmie>();
        var connectionString = string.Format(TestUtilities.LoadConfiguration().GetConnectionString(TestData.DefaultTestConnectionStringAlias),
            typeof(Cliente).Name);
        
        // Act
        fixture.SetWorkWithSqlServer();

        // Assert
        fixture.Context.Should().NotBeNull();
        fixture.Context.Database.IsInMemory().Should().BeFalse();
        fixture.Context.Database.GetPendingMigrations().Should().BeEmpty();
        fixture.Context.Database.GetAppliedMigrations().Should().NotBeEmpty();
        fixture.Context.Database.GetConnectionString().Should().Be(connectionString);
        fixture.Context.GetType().GetProperties().Should().Contain(p => p.PropertyType == typeof(DbSet<Cliente>));
        fixture.Context.GetType().GetProperties().Should().Contain(p => p.PropertyType == typeof(DbSet<Venda>));
    }

    [Fact]
    public void DatabaseFixture_SetupDataContextSqlServer_WhenInMemory()
    {
        // Arrange
        var fixture = new DatabaseFixture<Cliente, DbContextOmie>();
        var connectionString = string.Format(TestUtilities.LoadConfiguration().GetConnectionString(TestData.DefaultTestConnectionStringAlias),
            typeof(Cliente).Name);
        
        // Act
        fixture.SetWorkWithSqlServer();

        // Assert
        fixture.Context.Should().NotBeNull();
        fixture.Context.Database.IsInMemory().Should().BeTrue();
        fixture.Context.Database.GetConnectionString().Should().NotBe(connectionString);
        fixture.Context.Database.GetConnectionString().Should().Contain("InMemory");
        ((DbContextOmie)fixture.Context).GetType().GetProperties().Should().Contain(p => p.PropertyType == typeof(DbSet<Cliente>));
        ((DbContextOmie)fixture.Context).GetType().GetProperties().Should().Contain(p => p.PropertyType == typeof(DbSet<Venda>));
    }

}