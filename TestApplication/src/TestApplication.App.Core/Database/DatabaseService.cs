using System.Data;
using System.Data.SqlClient;
using TestApplication.DesktopApp.Core.Consts;
using TestApplication.DesktopApp.Core.Interfaces;

namespace TestApplication.DesktopApp.Core.Database;

public class DatabaseService : IDatabaseService
{
    private readonly string _databaseName;

    private string _connectionString = DatabaseConsts.DefaultConnectionString;

    public DatabaseService(string databaseName)
    {
        _databaseName = databaseName;
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

    public string GetConnectionString()
    {
        _connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database={_databaseName};Trusted_Connection=True;MultipleActiveResultSets=true";
        return _connectionString;
    }

    public async Task CreateDatabaseAsync()
    {
        using var connection = new SqlConnection(_connectionString);

        var query = $"CREATE DATABASE [{_databaseName}];";

        await connection.OpenAsync();
        var command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();

        connection.ChangeDatabase(_databaseName);

        query = "CREATE TABLE [" + _databaseName + @"].dbo.Table_A
                     (
                        Col_A1 int NOT NULL,
                        Col_A2 varchar(10) NULL,
                        Col_A3 date NOT NULL,
                     ); ";
        command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();

        query = "CREATE TABLE [" + _databaseName + @"].dbo.Table_B
                     (
                        Col_B1 int NOT NULL,
                        Col_B2 nchar(10) NULL,
                        Col_B3 int NOT NULL,
                     );";
        command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();

        query = "CREATE TABLE [" + _databaseName + @"].dbo.Table_C
                     (
                        Col_C1 varchar(10) NULL,
                        Col_C2 time(7) NOT NULL,
                        Col_C3 int NOT NULL,
                        Col_C4 char(10) NULL,
                     );";
        command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();

        await InsertDataAsync();
    }

    private async Task InsertDataAsync()
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();
        await connection.ChangeDatabaseAsync(_databaseName);

        var query = "INSERT INTO Table_A\n VALUES (23, 'test1', '2023-10-10')\n" +
                    "INSERT INTO Table_A\n VALUES (18, 'test2', '2023-11-10')\n";

        var command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();

        query = "INSERT INTO Table_B\n VALUES (09, 'temp11', 5)\n" +
            "INSERT INTO Table_B\n VALUES (2345, 'temp22', 9)\n";

        command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();

        query = "INSERT INTO Table_C\n VALUES ('data', '10:10:11', 456, 'teoric')\n" +
            "INSERT INTO Table_C\n VALUES ('next gen', '23:19:55', 267, 'rftyd')\n";

        command = new SqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();
    }
}
