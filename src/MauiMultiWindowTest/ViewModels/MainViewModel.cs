using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMultiWindowTest.Common;
using MauiMultiWindowTest.Pages;
using MauiMultiWindowTest.Services;
namespace MauiMultiWindowTest.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IShellNavigationService _shellNavigationService;
        private readonly IWindowService _windowService;
        private int _count = 0;
        private string _counterBtnText = "Click me";
        private int _openedWindowCount;
        private string _welcomeMessage;

        public MainViewModel(IShellNavigationService shellNavigationService, IWindowService windowService)
        {
            _shellNavigationService = shellNavigationService;
            _windowService = windowService;

            this.CounterBtnCommand = new RelayCommand(ExecuteCounter);
            this.NavigateToInShellPageCommand = new AsyncRelayCommand(GotoInShellPageAsync);
            this.ShowFixedWindowCommand = new RelayCommand(ShowFixedWindow);
            this.ShowMultipleFixedWindowsCommand = new RelayCommand(ShowMultipleFixedWindows);
            this.CloseAllSecondaryWindowsCommand = new RelayCommand(CloseAllSecondaryWindows);

            this.WelcomeMessage = $"Testing multiple application windows on {DeviceInfo.Current.Platform}";

        }
        
        
        private void ShowFixedWindow()
        {
            SecondaryPageViewModel vm = ServiceHelper.GetService<SecondaryPageViewModel>();
            vm.ParentWindowTitle = "Single fixed window";
            vm.ParentWindowKey = "SingleFixedWindow";
            
            _windowService.ShowWindowForPage<SecondaryPage, SecondaryPageViewModel>(vm);
        }
        
        private void ShowMultipleFixedWindows()
        { 
            SecondaryPageViewModel vm = ServiceHelper.GetService<SecondaryPageViewModel>();
            
            int id = _openedWindowCount++;
            vm.ParentWindowKey = $"MultiFixedWindow{id}";
            vm.ParentWindowTitle = $"Multi Fixed Window {id}";
            
            _windowService.ShowWindowForPage<SecondaryPage, SecondaryPageViewModel>(vm);
        }

        private void CloseAllSecondaryWindows() =>
            _windowService.CloseAllSecondaryWindows();

        private async Task GotoInShellPageAsync() =>
            await _shellNavigationService.NavigateToRouteAsync(Constants.InShellPageRoute);

        private void ExecuteCounter()
        {
            _count++;

            if (_count == 1)
                this.CounterBtnText = $"Clicked {_count} time";
            else
                this.CounterBtnText = $"Clicked {_count} times";
        }


        

        public string CounterBtnText
        {
            get => _counterBtnText;
            set => SetProperty(ref _counterBtnText, value);
        }

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set => SetProperty(ref _welcomeMessage, value);
        }

        public RelayCommand CounterBtnCommand { get; }
        public AsyncRelayCommand NavigateToInShellPageCommand { get; }
        public RelayCommand ShowFixedWindowCommand { get; }
        public RelayCommand ShowMultipleFixedWindowsCommand { get; }
        public RelayCommand CloseAllSecondaryWindowsCommand { get; }
    }
}
