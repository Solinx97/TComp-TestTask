using MvvmCross.Platforms.Wpf.Views;
using System.Windows.Controls;

namespace TestApplication.DesktopApp.Views;

public partial class HomeView : MvxWpfView
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext != null)
        {
            ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword;
        }
    }
}
