using System.Data;
using System.Data.SqlClient;
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

    public async Task<IEnumerable<RetrievedDataModel>> GetDataAsync(UserModel user)
    {
        var result = new List<RetrievedDataModel>();

        var data = await GetDataOnlyByIntAsync(user, "Table_A");
        result.AddRange(data);

        data = await GetDataOnlyByIntAsync(user, "Table_B");
        result.AddRange(data);

        data = await GetDataOnlyByIntAsync(user, "Table_C");
        result.AddRange(data);

        return result;
    }

    private async Task<IEnumerable<RetrievedDataModel>> GetDataOnlyByIntAsync(UserModel user, string tableName)
    {
        using var connection = new SqlConnection(_connectionString);

        var query = $"SELECT * FROM {tableName}";
        var command = new SqlCommand(query, connection);

        connection.Credential = new SqlCredential(user.Login, user.Password);
        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows)
        {
            return new List<RetrievedDataModel>();
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

        while (await reader.ReadAsync())
        {
            for (int i = 0; i < databseData.Count; i++)
            {
                databseData[i].Values.Add(reader.GetInt32(databseData[i].ColumnName));
            }
        }

        return databseData;
    }
}
