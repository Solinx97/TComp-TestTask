using MvvmCross.ViewModels;
using TestApplication.DesktopApp.Core.ViewModels;

namespace TestApplication.DesktopApp.Core;

public class App : MvxApplication
{
    public override void Initialize()
    {
        RegisterAppStart<HomeViewModel>();
    }
}
