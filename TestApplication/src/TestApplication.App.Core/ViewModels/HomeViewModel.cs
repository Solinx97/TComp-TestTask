using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using System.Security;
using TestApplication.DesktopApp.Core.Database;
using TestApplication.DesktopApp.Core.Models;

namespace TestApplication.DesktopApp.Core.ViewModels;

public class HomeViewModel : MvxViewModel
{
    private ObservableCollection<RetrievedDataModel> _retrievedData;
    private bool _isConnected;
    private bool _isLoaded;
    private string _login = string.Empty;
    private SecureString _password = new SecureString();

    private DatabaseRepository _databaseRepository;

    public HomeViewModel()
    {
        TestConnectionCommand = new MvxAsyncCommand(ConnectionAsync);
        LoadDataCommand = new MvxAsyncCommand(LoadDataAsync);
    }

    public IMvxAsyncCommand TestConnectionCommand { get; set; }

    public IMvxAsyncCommand LoadDataCommand { get; set; }

    public ObservableCollection<RetrievedDataModel> RetrievedData
    {
        get { return _retrievedData; }
        set
        {
            SetProperty(ref _retrievedData, value);
            if (value.Any())
            {
                IsLoaded = true;
            }
            else
            {
                IsLoaded = false;
            }
        }
    }

    public bool IsConnected
    {
        get { return _isConnected; }
        set
        {
            SetProperty(ref _isConnected, value);
        }
    }

    public bool IsLoaded
    {
        get { return _isLoaded; }
        set
        {
            SetProperty(ref _isLoaded, value);
        }
    }

    public string Login
    {
        get { return _login; }
        set
        {
            SetProperty(ref _login, value);
        }
    }

    public SecureString Password
    {
        get 
        {
            _password.MakeReadOnly();
            return _password; 
        }
        set
        {
            SetProperty(ref _password, value);
        }
    }

    public async Task ConnectionAsync()
    {
        var user = new UserModel { Login = Login, Password = Password };

        _databaseRepository = new DatabaseRepository("DevData");
        await _databaseRepository.ConnectionAsync();

        IsConnected = true;
    }

    public async Task LoadDataAsync()
    {
        var user = new UserModel { Login = Login, Password = Password };
        var data = await _databaseRepository.GetDataAsync(user);

        RetrievedData = new ObservableCollection<RetrievedDataModel>(data.ToList());
    }
}
