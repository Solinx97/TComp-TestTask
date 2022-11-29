namespace TestApplication.DesktopApp.Core.Interfaces;

public interface IDatabaseService
{
    Task<bool> IsExistAsync();

    Task CreateDatabaseAsync();

    string GetConnectionString();
}
