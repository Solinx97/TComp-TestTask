using Microsoft.Extensions.DependencyInjection;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using TestApplication.BL.DTO;
using TestApplication.BL.Interfaces;
using TestApplication.DesktopApp.Core.Database;
using TestApplication.DesktopApp.Core.Models;

namespace TestApplication.DesktopApp.Core.ViewModels;

public class HomeViewModel : MvxViewModel
{
    //private readonly IService<TableADTO, int> _tableAService;

    private ObservableCollection<RetrievedDataModel> _retrievedData;

    public HomeViewModel(IServiceProvider provider)
    {
        //_tableAService = provider?.GetService<IService<TableADTO, int>>();

        ConnectionToDBCommand = new MvxAsyncCommand(GetDataAsync);
    }

    public IMvxAsyncCommand ConnectionToDBCommand { get; set; }

    public ObservableCollection<RetrievedDataModel> RetrievedData
    {
        get { return _retrievedData; }
        set
        {
            SetProperty(ref _retrievedData, value);
        }
    }

    public async Task GetDataAsync()
    {
        var databaseRepository = new DatabaseRepository("DevData");
        await databaseRepository.ConnectionAsync();
        await databaseRepository.GetDataAsync();
    }
}
