using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Omie.Domain.Abstractions;
using Tests.Common.Data;
using Omie.Application.Models.Abstractions;
using Omie.Domain.Entities;
using System.Reflection;
using AutoFixture.Dsl; // Add this line if ResourceDtoBase is in Omie.Domain.Dtos namespace


namespace Tests.Common.Fixtures;

public class DatabaseFixture<TEntity, TDbContext> : IDisposable where TDbContext : DbContext where TEntity : EntityBase
{
    public TDbContext Context { get; private set; }
    private readonly Random _random = new();
    private bool _workInMemory = true;

    public DatabaseFixture()
    {
        if (_workInMemory) 
        {
            Context = TestData.CreateInMemoryDbContext<TDbContext>();
        } else 
        {
            Context = TestData.CreateSQlServerTestDbContext<TDbContext, TEntity>();
        }

        SetupDataContextSqlServer();
    }

    public void SetWorkWithSqlServer()
    {
        _workInMemory = false;
        Context = TestData.CreateSQlServerTestDbContext<TDbContext, TEntity>();
        SetupDataContextSqlServer();
    }

    private void SetupDataContextSqlServer()
    {
        if (Context.Database.IsInMemory())
            return;
        
        // Ensure the database is clean before running tests
        Context.Database.EnsureDeleted();
        Context.Database.Migrate(); // Apply migrations
    }

    public void SeedData<TKey>(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            Context.Add(entity);
        }
        Context.SaveChanges();
    }

    public void SeedDataSingle<TKey>(EntityBaseRoot<TKey> entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public List<T> GetEntities<T>(int howMany, bool ignoreNulldef) where T : EntityBase
    {
        var returnList = new List<T>();
        for (int i = 0; i < howMany; i++)
        {
            T entity = GetEntityFilled<T>();
            returnList.Add(entity);
        }

        return returnList;
    }

    public T GetEntityFilled<T>() where T : EntityBase
    {
        
        var fixture = new Fixture();
        var type = typeof(T);
        var property = type.GetProperty("Id");
        if (property != null)
        {
            fixture.Customize<T>(c => c
                .Without(x => ((EntityBaseRoot<long>)(object)x).Id)
                );
        }
        
        return fixture.Create<T>();
    }
    
    public List<Venda> GetVendas(int howMany)
    {
        var returnList = new List<Venda>();
        for (int i = 0; i < howMany; i++)
        {
            var dataVenda = DateTime.Now.AddYears(-_random.Next(0, 5)).AddDays(-_random.Next(0, 30));
            var nomeCliente = new Fixture().Create<string>();
            var entity = new Venda
            {
                DataDaVenda = dataVenda, // Random date within the last 5 years and 30 days
                Cliente = nomeCliente,
                CodigoVenda = $"{dataVenda:yyyyMMddHHmmss}-{nomeCliente.Replace(" ", "-").ToUpper()}",
                Itens = GetItensVenda(_random.Next(1, 10))
            };
            returnList.Add(entity);
        }

        return returnList;
    }

    public Cliente BuildCliente()
    {
        var fixture = new Fixture();
        var entity = fixture.Build<Cliente>()
            .With(c => c.Nome, $"Cliente Teste {Guid.NewGuid()}")
            .With(c => c.CPF, GenerateCpf())
            .With(c => c.Email, $"test@{Guid.NewGuid()}.com")
            .With(c => c.Telefone, $"{_random.Next(10, 100)} {_random.Next(10, 100)} {_random.Next(10000, 100000)} {_random.Next(1000, 10000)}")
            .With(c => c.Endereco, new Endereco
            {
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP",
            CEP = "12345678"
            })
            .With(c => c.Observacao, $"Observacao Teste {Guid.NewGuid()}")
            .Create();

        return entity;
    }

    public string GenerateCpf()
    {
        var random = new Random();
        var n = 9;
        var n1 = random.Next(0, n);
        var n2 = random.Next(0, n);
        var n3 = random.Next(0, n);
        var n4 = random.Next(0, n);
        var n5 = random.Next(0, n);
        var n6 = random.Next(0, n);
        var n7 = random.Next(0, n);
        var n8 = random.Next(0, n);
        var n9 = random.Next(0, n);
        var d1 = n9 * 2 + n8 * 3 + n7 * 4 + n6 * 5 + n5 * 6 + n4 * 7 + n3 * 8 + n2 * 9 + n1 * 10;
        d1 = 11 - (d1 % 11);
        if (d1 >= 10) d1 = 0;
        var d2 = d1 * 2 + n9 * 3 + n8 * 4 + n7 * 5 + n6 * 6 + n5 * 7 + n4 * 8 + n3 * 9 + n2 * 10 + n1 * 11;
        d2 = 11 - (d2 % 11);
        if (d2 >= 10) d2 = 0;
        return $"{n1}{n2}{n3}.{n4}{n5}{n6}.{n7}{n8}{n9}-{d1}{d2}";
    }

    public List<Item> GetItensVenda(int howMany)
    {
        var returnList = new List<Item>();
        for (int i = 0; i < howMany; i++)
        {
            var entity = new Item
            {
                Produto = new Fixture().Create<string>(),
                ValorUnitario = new Fixture().Create<decimal>(),
                Quantidade = (short)_random.Next(1, 10)
            };
            returnList.Add(entity);
        }

        return returnList;
    }
    
    public Produto BuildProduto()
    {
        var fixture = new Fixture();
        var entity = fixture.Build<Produto>()
            .With(p => p.Nome, $"Produto Teste {Guid.NewGuid()}")
            .With(p => p.Descricao, $"Descricao Teste {Guid.NewGuid()} - {Guid.NewGuid()} | {Guid.NewGuid()}")
            .With(p => p.Valor, Convert.ToDecimal(_random.NextDouble() * 100))
            .With(p => p.Imagem, $"https://mywebsite.com/image_{Guid.NewGuid()}.jpg")
            .With(p => p.Categoria, $"Categoria Teste {Guid.NewGuid()}")
            .With(p => p.Marca, $"Marca Teste {Guid.NewGuid()}")
            .With(p => p.Unidade, new string(Enumerable.Range(0, 2).Select(_ => (char)_random.Next('A', 'Z' + 1)).ToArray()))
            .With(p => p.Tipo, $"Tipo Teste {Guid.NewGuid()}")
            .With(p => p.Codigo, Guid.NewGuid().ToString().Substring(0, 12))
            .Create();
    
        return entity;
    }
    public void Dispose()
    {
        Context?.Dispose();
        GC.SuppressFinalize(this);
    }
}

