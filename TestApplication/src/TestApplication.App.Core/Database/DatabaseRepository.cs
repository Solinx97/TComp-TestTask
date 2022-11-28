using Microsoft.Data.SqlClient;
using System.Data;
using TestApplication.DesktopApp.Core.Models;

namespace TestApplication.DesktopApp.Core.Database;

internal class DatabaseRepository
{
	private readonly string _databaseName;

	private string _connectionString;

	public DatabaseRepository(string databaseName)
    {
        _databaseName = databaseName;
    }

    public async Task ConnectionAsync()
    {
        var database = new CreateDatabase(_databaseName);
        if (!await database.IsExistAsync())
        {
            await database.CreateDatabaseAsync();
        }

        _connectionString = database.GetConnectionString();
    }

    public async Task<List<RetrievedDataModel>> GetDataAsync()
    {
        using var connection = new SqlConnection(_connectionString);

        var query = "SELECT * FROM Table_A, Table_B, Table_C";

        var command = new SqlCommand(query, connection);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows)
        {
            return null;
        }

        var databseData = new List<RetrievedDataModel>();

        for (int i = 0; i < reader.FieldCount; i++)
        {
            var typeName = reader.GetDataTypeName(i);
            if (typeName == "int")
            {
                var retrievedData = new RetrievedDataModel { ColumnName = reader.GetName(i), Values = new List<int>() };
                databseData.Add(retrievedData);
            }
        }

        var count = 0;
        while (await reader.ReadAsync())
        {
            for (int i = 0; i < databseData.Count; i++)
            {
                databseData[i].Values.Add(reader.GetInt32(databseData[i].ColumnName));
            }

            count++;
        }

        return databseData;
    }
}
