using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;
using System.Windows;

namespace TestApplication.DesktopApp;

public partial class App : MvxApplication
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
    }

    protected override void RegisterSetup()
    {
        base.RegisterSetup();
        this.RegisterSetupType<MvxWpfSetup<Core.App>>();
    }
}
