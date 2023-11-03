using MauiMultiWindowTest.Pages;
using MauiMultiWindowTest.ViewModels;
using MauiMultiWindowTest.Windows;
namespace MauiMultiWindowTest.Services
{
    public interface IWindowService
    {
        Dictionary<string, SecondaryWindow> CurrentlyOpenedWindows { get; }

        void ShowWindowForPage<TPageType, TViewModelType>(TViewModelType vm)
            where TPageType : SecondaryWindowBasePage
            where TViewModelType : SecondaryWindowPageViewModelBase;
        
        void CloseWindow(string? windowKey);

        void CloseAllSecondaryWindows();

        SecondaryWindow? GetByKey(string? key);
    }
}
