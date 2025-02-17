
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Omie.WebApi;
using Tests.Common;
using Microsoft.Extensions.Configuration;

namespace Database.Tests;


public class DbConnectionTests
{
    private readonly string _connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=My#Stron8P4ssw0rd;TrustServerCertificate=True;";

    [Fact]
    public async Task Should_Connect_To_Database_Successfully()
    {
        var config = TestUtilities.LoadConfiguration<VendaController>();
        var strConnection = config.GetConnectionString("DefaultConnection");
        await using var connection = new SqlConnection(strConnection);
        
        Func<Task> act = async () => await connection.OpenAsync();
        
        await act.Should().NotThrowAsync();
        connection.State.Should().Be(ConnectionState.Open);
    }

    [Fact]
    public async Task Should_Execute_Simple_Query_Successfully()
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT 1";
        
        var result = await command.ExecuteScalarAsync();
        
        result.Should().NotBeNull();
        result.Should().BeOfType<int>();
        result.Should().Be(1);
    }

    [Fact]
    public async Task Should_Insert_And_Retrieve_Data_Correctly()
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        // Create test table
        await using var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = @"
            IF OBJECT_ID('TestTable', 'U') IS NOT NULL DROP TABLE TestTable;
            CREATE TABLE TestTable (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(100));";
        await createTableCommand.ExecuteNonQueryAsync();

        // Insert data
        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = "INSERT INTO TestTable (Name) VALUES (@name)";
        insertCommand.Parameters.AddWithValue("@name", "Test Entry");
        await insertCommand.ExecuteNonQueryAsync();

        // Read data
        await using var selectCommand = connection.CreateCommand();
        selectCommand.CommandText = "SELECT Name FROM TestTable WHERE Id = 1";
        var result = await selectCommand.ExecuteScalarAsync();

        result.Should().NotBeNull();
        result.Should().Be("Test Entry");
    }
}
