using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.Extensions;
using ShopSphere.Services;
using ShopSphere.ViewModels;
using ShopSphere.Views;

namespace ShopSphere;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    private ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddCommonServices();
        return services.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = ConfigureServices();
        
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {

            var nav = services.GetService<IAuthNavigationService>();
            nav.AuthNavigateTo<LoginVM>();
            desktop.MainWindow = new AuthWindow
            {
                DataContext = nav
        };
        }
        

        base.OnFrameworkInitializationCompleted();
    }
    
}
