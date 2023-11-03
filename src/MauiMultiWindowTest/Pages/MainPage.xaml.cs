using MauiMultiWindowTest.ViewModels;
using MauiMultiWindowTest.Windows;
namespace MauiMultiWindowTest.Pages;

public partial class MainPage : ResizeableMacBasePage
{
    

    public MainPage(MainViewModel mainVm)
    {
        InitializeComponent();

        this.BindingContext = mainVm;
        
    }


}
