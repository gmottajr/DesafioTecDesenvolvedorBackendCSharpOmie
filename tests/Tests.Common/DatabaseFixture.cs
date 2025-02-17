using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Omie.Domain.Abstractions;
using Omie.WebApi;
using Xunit;

namespace Tests.Common.Fixtures;

public class DatabaseFixture<TDbContex, TControllerForAssemblyRef> : IDisposable where TDbContex : DbContext where TControllerForAssemblyRef : ControllerBase
{
    public TDbContex Context { get; private set; }
    public DatabaseFixture()
    {
        var config = TestUtilities.LoadConfiguration<TControllerForAssemblyRef>();
        var connectionString = config.GetConnectionString("TestDbConnectionString");
        var options = new DbContextOptionsBuilder<TDbContex>()
            .UseSqlServer(connectionString)
            .Options;
        Context = (TDbContex)Activator.CreateInstance(typeof(TDbContex), options);
        // Ensure the database is clean before running tests
        Context.Database.EnsureDeleted();
        Context.Database.Migrate(); // Apply migrations
    }


    public void SeedData<TKey>(List<EntityBaseRoot<TKey>> entities)
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

    public List<TEntity> GetEntities<TEntity>(int howMany, bool ignoreNulldef) where TEntity : EntityBase
    {
        var returnList = new List<TEntity>();
        for (int i = 0; i < howMany; i++)
        {
            TEntity? entity = GetEntityFilled<TEntity>(ignoreNulldef, i);
            returnList.Add(entity);
        }

        return returnList;
    }

    public TEntity GetEntityFilled<TEntity>(bool ignoreNulldef, int i) where TEntity : EntityBase
    {
        var entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
        var model = Context.Model;
        var entityType = model.FindEntityType(typeof(TEntity));

        
        var properties = entity.GetType().GetProperties();
        if (entityType != null)
        {
            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(string))
                {
                    string value = Guid.NewGuid().ToString().Replace("-", "");
                    prop.SetValue(entity, value);
                }
                else if (Nullable.GetUnderlyingType(prop.PropertyType) == typeof(string))
                {
                if (!ignoreNulldef)
                {
                    prop.SetValue(entity, null);
                }
                else
                {
                    prop.SetValue(entity, Guid.NewGuid().ToString());
                }
                }
                else if (prop.PropertyType == typeof(Guid))
                {
                prop.SetValue(entity, Guid.NewGuid());
                }
                else if (prop.PropertyType == typeof(Guid?))
                {
                if (!ignoreNulldef)
                {
                    prop.SetValue(entity, null);
                }
                else
                {
                    prop.SetValue(entity, Guid.NewGuid());
                }
                }
                else if (prop.PropertyType.IsEnum)
                {
                prop.SetValue(entity, Enum.GetValues(prop.PropertyType).GetValue(0));
                }
                else if (Nullable.GetUnderlyingType(prop.PropertyType)?.IsEnum == true)
                {
                if (!ignoreNulldef)
                {
                    prop.SetValue(entity, null);
                }
                else
                {
                    prop.SetValue(entity, Enum.GetValues(Nullable.GetUnderlyingType(prop.PropertyType)).GetValue(0));
                }
                }
                else if (prop.PropertyType == typeof(int))
                {
                prop.SetValue(entity, i);
                }
                else if (prop.PropertyType == typeof(int?))
                {
                if (!ignoreNulldef)
                {
                    prop.SetValue(entity, null);
                }
                else
                {
                    prop.SetValue(entity, i);
                }
                }
                else if (prop.PropertyType == typeof(long))
                {
                    prop.SetValue(entity, i);
                }
                else if (prop.PropertyType == typeof(long?))
                {
                if (!ignoreNulldef)
                {
                    prop.SetValue(entity, null);
                }
                else
                {
                    prop.SetValue(entity, i);
                }
                }
                else if (prop.PropertyType == typeof(decimal))
                {
                prop.SetValue(entity, i);
                }
                else if (prop.PropertyType == typeof(decimal?))
                {
                if (!ignoreNulldef)
                {
                    prop.SetValue(entity, null);
                }
                else
                {
                    prop.SetValue(entity, i);
                }
                }
                else if (prop.PropertyType == typeof(bool))
                {
                prop.SetValue(entity, true);
                }
                else if (prop.PropertyType == typeof(bool?))
                {
                if (!ignoreNulldef)
                {
                    prop.SetValue(entity, null);
                }
                else
                {
                    prop.SetValue(entity, true);
                }
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    prop.SetValue(entity, DateTime.Now);
                }
                else if (prop.PropertyType == typeof(DateTime?))
                {
                    if (!ignoreNulldef)
                    {
                        prop.SetValue(entity, null);
                    }
                    else
                    {
                        prop.SetValue(entity, DateTime.Now);
                    }
                }
                else if (prop.PropertyType == typeof(EntityBase))
                {
                var nestedEntity = GetEntityFilled<EntityBase>(ignoreNulldef, i);
                prop.SetValue(entity, nestedEntity);
                }
                else if (typeof(EntityBase).IsAssignableFrom(prop.PropertyType))
                {
                    var nestedEntity = (EntityBase)Activator.CreateInstance(prop.PropertyType);
                    var method = typeof(DatabaseFixture<TDbContex, TControllerForAssemblyRef>).GetMethod(nameof(GetEntityFilled)).MakeGenericMethod(prop.PropertyType);
                    nestedEntity = (EntityBase)method.Invoke(this, new object[] { ignoreNulldef, i });
                    prop.SetValue(entity, nestedEntity);
                }
            }
        
        }
        return entity;
    }

    public void Dispose()
    {
        Context?.Dispose();
    }
}   


