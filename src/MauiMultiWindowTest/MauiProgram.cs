using CommunityToolkit.Maui;
using MauiMultiWindowTest.Common;
using MauiMultiWindowTest.Pages;
using MauiMultiWindowTest.Services;
using MauiMultiWindowTest.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiMultiWindowTest;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.
            UseMauiApp<App>().UseMauiCommunityToolkit().
                ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        RegisterServices(builder.Services);
        RegisterViewModels(builder.Services);
        RegisterPages(builder.Services);
        
        
        return builder.Build();
    }
    
    private static void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IShellNavigationService, ShellNavigationService>();
        serviceCollection.AddSingleton<IWindowService, WindowService>();
    }
    
    private static void RegisterViewModels(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MainViewModel>();
        serviceCollection.AddTransient<SecondaryPageViewModel>();
    }
    
    private static void RegisterPages(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<AppShell>();
        serviceCollection.AddSingleton<MainPage>();
        serviceCollection.AddTransient<SecondaryPage>();
        
        serviceCollection.AddSingletonWithShellRoute<InShellPage, InShellPageViewModel>(Constants.InShellPageRoute);
    }
}
