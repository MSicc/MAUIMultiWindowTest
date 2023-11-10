using MauiMultiWindowTest.ViewModels;
using MauiMultiWindowTest.Windows;
namespace MauiMultiWindowTest.Pages;

public partial class MainPage : ResizeableDesktopBasePage
{
    

    public MainPage(MainViewModel mainVm)
    {
        InitializeComponent();

        this.BindingContext = mainVm;
        
    }


}
