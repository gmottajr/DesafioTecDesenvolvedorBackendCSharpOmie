using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Tests.Common.Data;
using Omie.Domain.Entities;
using Omie.DAL;

namespace Tests.Common
{
    public class TestDataHelperTests
    {
        [Fact]
        public void CreateInMemoryDbContext_ShouldReturnDbContextInstance()
        {
            // Act
            var context = TestData.CreateInMemoryDbContext<DbContextOmie>();

            // Assert
            context.Should().NotBeNull();
            context.Should().BeOfType<DbContextOmie>();
            context.Database.IsInMemory().Should().BeTrue();

        }

        [Fact]
        public void CreateSQlServerTestDbContext_ShouldReturnDbContextInstance()
        {
            // Act
            var context = TestData.CreateSQlServerTestDbContext<DbContextOmie, Cliente>();

            // Assert
            context.Should().NotBeNull();
            context.Should().BeOfType<DbContextOmie>();
        }
    }
}