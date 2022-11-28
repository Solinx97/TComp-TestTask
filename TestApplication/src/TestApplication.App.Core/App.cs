using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MvvmCross;
using MvvmCross.ViewModels;
using System.Configuration;
using TestApplication.BL.Extensions;
using TestApplication.BL.Mapping;
using TestApplication.DesktopApp.Core.ViewModels;

namespace TestApplication.DesktopApp.Core;

public class App : MvxApplication
{
    public override void Initialize()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new BLMapper());
        });
        var mapper = mappingConfig.CreateMapper();

        var connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        var services = new ServiceCollection();
        services.AddSingleton(mapper);
        services.RegisterDependenciesForBL(connectionString);
        var provider = services.BuildServiceProvider();

        Mvx.IoCProvider.RegisterSingleton<IServiceProvider>(provider);

        RegisterAppStart<HomeViewModel>();
    }
}
