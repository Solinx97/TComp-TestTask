using MvvmCross.ViewModels;
using System.Configuration;
using TestApplication.DesktopApp.Core.ViewModels;

namespace TestApplication.DesktopApp.Core;

public class App : MvxApplication
{
    public override void Initialize()
    {
        var connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        RegisterAppStart<HomeViewModel>();
    }
}
