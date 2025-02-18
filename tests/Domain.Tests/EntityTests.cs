using System.Reflection;
using FluentAssertions;
using Omie.Domain.Abstractions;
using Omie.Domain.Entities;

namespace Domain.Tests;

public class EntityTests
{
    private readonly Type _entityBaseType = typeof(EntityBase);
    private readonly Type _entityBaseRootType = typeof(EntityBaseRoot<>);

    [Fact]
    public void All_classes_in_Omie_Domain_Entities_Should_Inherit_From_EntityBase_Or_EntityBaseRoot()
    {
        var entityType = typeof(Cliente); // Using an existing entity to load the assembly
        var domainAssembly = Assembly.GetAssembly(entityType)!;
        var entityNamespace = entityType.Namespace!;

        var entityTypes = domainAssembly.GetTypes()
            .Where(t => t.Namespace == entityNamespace && t.IsClass && !t.IsAbstract);

        foreach (var type in entityTypes)
        {
            bool inheritsFromEntityBase = _entityBaseType.IsAssignableFrom(type);
            bool inheritsFromEntityBaseRoot = type.BaseType != null && type.BaseType.IsGenericType &&
                                              type.BaseType.GetGenericTypeDefinition() == _entityBaseRootType;

            (inheritsFromEntityBase || inheritsFromEntityBaseRoot)
                .Should().BeTrue($"Class {type.Name} must inherit from EntityBase or EntityBaseRoot.");
        }
    }
}
