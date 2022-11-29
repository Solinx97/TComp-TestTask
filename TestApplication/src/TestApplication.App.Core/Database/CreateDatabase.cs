using System.Data;
using System.Data.SqlClient;
using TestApplication.DesktopApp.Core.Interfaces;

namespace TestApplication.DesktopApp.Core.Database;

public class CreateDatabase : IDatabaseService
{
    private readonly string _connectionString;
    private readonly string _databaseName;

    public CreateDatabase(string databaseName)
    {
        _databaseName = databaseName;

        _connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database=master;Trusted_Connection=False;MultipleActiveResultSets=true";
    }

    public async Task<bool> IsExistAsync()
    {
        using var connection = new SqlConnection(_connectionString);

        var query = $"IF DB_ID('{_databaseName}') IS NOT NULL\n" +
                        $"SET @IsExist='True'\n" +
                     $"ELSE\n" +
                        $"SET @IsExist='False'";

        var idParam = new SqlParameter
        {
            ParameterName = "@isExist",
            SqlDbType = SqlDbType.Bit,
            Direction = ParameterDirection.Output,
        };

        await connection.OpenAsync();

        var command = new SqlCommand(query, connection);
        command.Parameters.Add(idParam);
        await command.ExecuteNonQueryAsync();

        return (bool)idParam.Value;
    }

    public async Task CreateDatabaseAsync()
    {
        using var connection = new SqlConnection(_connectionString);

        var query = $"CREATE DATABASE [{_databaseName}];";

        await connection.OpenAsync();
        var command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();

        await CreateTableAAsync(connection);
        await CreateTableBAsync(connection);
        await CreateTableCAsync(connection);
    }

    public string GetConnectionString()
    {
        var connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database={_databaseName};Trusted_Connection=True;MultipleActiveResultSets=true";
        return connectionString;
    }

    private async Task CreateTableAAsync(SqlConnection connection)
    {
        var query = "CREATE TABLE [" + _databaseName + @"].dbo.Table_A
                     (
                        Col_A1 int NOT NULL,
                        Col_A2 varchar(10) NULL,
                        Col_A3 date NOT NULL,
                     );";
        var command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();
    }

    private async Task CreateTableBAsync(SqlConnection connection)
    {
        var query = "CREATE TABLE [" + _databaseName + @"].dbo.Table_B
                     (
                        Col_B1 int NOT NULL,
                        Col_B2 nchar(10) NULL,
                        Col_B3 int NOT NULL,
                     );";
        var command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();
    }

    private async Task CreateTableCAsync(SqlConnection connection)
    {
        var query = "CREATE TABLE [" + _databaseName + @"].dbo.Table_C
                     (
                        Col_C1 varchar(10) NULL,
                        Col_C2 time(7) NOT NULL,
                        Col_C3 int NOT NULL,
                        Col_C4 char(10) NULL,
                     );";
        var command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();
    }
}
