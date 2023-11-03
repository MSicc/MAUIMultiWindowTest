using MauiMultiWindowTest.Common;
using MauiMultiWindowTest.Pages;
using MauiMultiWindowTest.ViewModels;
using MauiMultiWindowTest.Windows;
namespace MauiMultiWindowTest.Services
{
    public class WindowService : IWindowService
    {
        public Dictionary<string, SecondaryWindow> CurrentlyOpenedWindows { get; } = new Dictionary<string, SecondaryWindow>();

        public void ShowWindowForPage<TPageType, TViewModelType>(TViewModelType vm) 
            where TPageType : SecondaryWindowBasePage 
            where TViewModelType : SecondaryWindowPageViewModelBase
        {
            //failing gracefully here
            if (this.CurrentlyOpenedWindows.ContainsKey(vm.ParentWindowKey))
                return;
            
            //failing hard here
            ArgumentNullException.ThrowIfNull(vm);
            ArgumentException.ThrowIfNullOrWhiteSpace(vm.ParentWindowKey);
            ArgumentException.ThrowIfNullOrWhiteSpace(vm.ParentWindowTitle);
                
            //this should be used with Transient registrations
            TPageType page = ServiceHelper.GetService<TPageType>();
            page.BindingContext = vm;

            SecondaryWindow windowToOpen = new SecondaryWindow(page, vm.ParentWindowTitle, vm.ParentWindowKey);
            
            windowToOpen.Created += (sender, args) => 
                this.CurrentlyOpenedWindows.Add(vm.ParentWindowKey, windowToOpen);

            windowToOpen.Destroying += (sender, args) =>
                this.CurrentlyOpenedWindows.Remove(vm.ParentWindowKey);
            
            Application.Current?.OpenWindow(windowToOpen);
        }

        public void CloseWindow(string? windowKey)
        {
            if (!this.CurrentlyOpenedWindows.TryGetValue(windowKey, out SecondaryWindow? value))
                return;
            
            Application.Current?.CloseWindow(value);
            this.CurrentlyOpenedWindows.Remove(windowKey);
        }

        public void CloseAllSecondaryWindows()
        {
            foreach (var key in this.CurrentlyOpenedWindows.Keys.ToList())
            {
                CloseWindow(key);
                this.CurrentlyOpenedWindows.Remove(key);
            }
        }

        public SecondaryWindow? GetByKey(string? key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;

            return !this.CurrentlyOpenedWindows.TryGetValue(key, out SecondaryWindow? value) ? null : value;

        }
    }
}
