using System.Security;

namespace TestApplication.DesktopApp.Core.Models;

public class UserModel
{
    public string Login { get; set; }

    public SecureString Password { get; set; }
}
