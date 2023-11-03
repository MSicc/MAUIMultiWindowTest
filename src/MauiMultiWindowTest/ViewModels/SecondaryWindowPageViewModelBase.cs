using CommunityToolkit.Mvvm.ComponentModel;
namespace MauiMultiWindowTest.ViewModels
{
    public abstract class SecondaryWindowPageViewModelBase : ObservableObject
    {
        private string? _parentWindowKey;
        private string? _parentWindowTitle;
        
        public string? ParentWindowKey   
        {
            get => _parentWindowKey;
            set => SetProperty(ref _parentWindowKey, value);
        }

        public string? ParentWindowTitle
        {
            get => _parentWindowTitle;
            set => SetProperty(ref _parentWindowTitle, value);
        }
    }
}
