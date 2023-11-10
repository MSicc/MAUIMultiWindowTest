using MauiMultiWindowTest.Common;
using MauiMultiWindowTest.ViewModels;
using MauiMultiWindowTest.Windows;
namespace MauiMultiWindowTest;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //DO NOT USE THE application MainPage object to initialize the app,
        //it will fail if there are multiple windows open
        //MainPage = new AppShell();
    }

    //this call is from the OS, you do not have any control over it!
    protected override Window CreateWindow(IActivationState activationState)
    {
        //Window window = base.CreateWindow(activationState);
        
        //in my tests, no matter how much data I save during the backgrounding event of the window, it never ever gets passed into this
        //best bet here is to save the data on our own and reload everything from there

        //https://github.com/dotnet/maui/issues/10939
        //the Page MUST ALWAYS BE A NEW INSTANCE
        //trying to get the Page from DI will throw an InvalidOperationException because
        //MauiContext is null
        
        var window = new PrimaryWindow(new AppShell
        {
            BindingContext = new AppShellViewModel()
        });

        TryToSizeAsOnLastRun(window);

        return window;
    }

    private void TryToSizeAsOnLastRun(PrimaryWindow window)
    {
        var lastknownMainWindowHeight = Preferences.Default.Get(Constants.SettingsLastKnownPrimaryWindowHeight, double.PositiveInfinity);
        var lastknwonMainWindowWidth =
            Preferences.Default.Get(Constants.SettingsLastKnownPrimaryWindowWidth, double.PositiveInfinity);

        window.Height = lastknownMainWindowHeight;
        window.Width = lastknwonMainWindowWidth;
    }
}
