using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tests.Common.Fixtures;
using Omie.Domain.Abstractions;
using Omie.Domain;
using Omie.Domain.Entities;

namespace Tests.Common.Data;

public static class TestData
{
    public static List<TEntity> GetEntities<TEntity, TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, int howMany = 1, bool ignoreNulldef = false)
        where TEntity : EntityBase
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        var entities = fixture.GetEntities<TEntity>(howMany, ignoreNulldef);
        return entities;
    }
    public static TEntity GetEntity<TEntity, TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, bool ignoreNulldef = false)
        where TEntity : EntityBase
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        var entity = fixture.GetEntityFilled<TEntity>(ignoreNulldef, 0);
        return entity;
    }
    // Example for specific entity like Cliente:
    public static List<Cliente> GetClientes<TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, int howMany = 2, bool ignoreNulldef = false)
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        return GetEntities<Cliente, TDbContex, TControllerForAssemblyRef>(fixture, howMany, ignoreNulldef);
    }
    public static Cliente GetCliente<TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, bool ignoreNulldef = false)
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        return GetEntity<Cliente, TDbContex, TControllerForAssemblyRef>(fixture, ignoreNulldef);
    }
    // Repeat the pattern for other entities like Enderecos, Produtos, etc.
    public static List<Endereco> GetEnderecos<TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, int howMany = 2, bool ignoreNulldef = false)
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        return GetEntities<Endereco, TDbContex, TControllerForAssemblyRef>(fixture, howMany, ignoreNulldef);
    }
    
    public static Endereco GetEndereco<TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, bool ignoreNulldef = false)
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        return GetEntity<Endereco, TDbContex, TControllerForAssemblyRef>(fixture, ignoreNulldef);
    }

    public static List<Produto> GetProdutos<TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, int howMany = 2, bool ignoreNulldef = false)
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        return GetEntities<Produto, TDbContex, TControllerForAssemblyRef>(fixture, howMany, ignoreNulldef);
    }

    public static Produto GetProduto<TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, bool ignoreNulldef = false)
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        return GetEntity<Produto, TDbContex, TControllerForAssemblyRef>(fixture, ignoreNulldef);
    }

    public static List<Venda> GetVendas<TDbContex, TControllerForAssemblyRef>(DatabaseFixture<TDbContex, TControllerForAssemblyRef> fixture, int howMany = 2, bool ignoreNulldef = false)
        where TDbContex : DbContext
        where TControllerForAssemblyRef : ControllerBase
    {
        return GetEntities<Venda, TDbContex, TControllerForAssemblyRef>(fixture, howMany, ignoreNulldef);
    }
}
