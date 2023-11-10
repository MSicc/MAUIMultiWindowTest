using MauiMultiWindowTest.ViewModels;
namespace MauiMultiWindowTest.Pages
{
    public class SecondaryWindowBasePage : ResizeableDesktopBasePage
    {
        public static BindableProperty ParentWindowKeyProperty =>
                BindableProperty.Create(nameof(SecondaryWindowBasePage.ParentWindowKey), typeof(string), typeof(SecondaryWindowBasePage));
        
        public SecondaryWindowBasePage()
        {
                
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext is not SecondaryWindowPageViewModelBase)
                return;

            SetBinding(TitleProperty, new Binding(nameof(SecondaryWindowPageViewModelBase.ParentWindowTitle), BindingMode.Default));
            SetBinding(SecondaryWindowBasePage.ParentWindowKeyProperty, new Binding(nameof(SecondaryWindowPageViewModelBase.ParentWindowKey), BindingMode.Default));
        }

        public string ParentWindowKey { get; set; }
    }
}
