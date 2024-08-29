using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using ShopSphere.Extensions;
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
            desktop.MainWindow = new AuthWindow
            {
                DataContext = services.GetRequiredService<AuthWindowVM>()
            };
        }
        

        base.OnFrameworkInitializationCompleted();
    }
    
}
