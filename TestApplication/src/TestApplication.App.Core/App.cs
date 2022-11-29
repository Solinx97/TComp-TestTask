using MvvmCross.ViewModels;
using System.Configuration;
using TestApplication.DesktopApp.Core.Consts;
using TestApplication.DesktopApp.Core.ViewModels;

namespace TestApplication.DesktopApp.Core;

public class App : MvxApplication
{
    public override void Initialize()
    {
        DatabaseConsts.DefaultConnectionString = ConfigurationManager.AppSettings.Get("defaultConnectionString");

        RegisterAppStart<HomeViewModel>();
    }
}
