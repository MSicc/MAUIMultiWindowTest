using MauiMultiWindowTest.Common;
using MauiMultiWindowTest.Windows;
namespace MauiMultiWindowTest.Pages
{
    public class ResizeableDesktopBasePage : ContentPage
    {
        public static readonly BindableProperty ParentWindowHeightProperty = 
            BindableProperty.Create(nameof(ResizeableDesktopBasePage.ParentWindowHeight), typeof(double), typeof(ResizeableDesktopBasePage), 
            double.PositiveInfinity, BindingMode.TwoWay, propertyChanged: OnDesiredWindowHeightChanged);

        public static readonly BindableProperty ParentWindowWidthProperty = 
            BindableProperty.Create(nameof(ResizeableDesktopBasePage.ParentWindowWidth), typeof(double), typeof(ResizeableDesktopBasePage), 
            double.PositiveInfinity, BindingMode.TwoWay, propertyChanged: OnDesiredWindowWidthChanged);

        public static readonly BindableProperty ParentWindowMinHeightProperty = 
            BindableProperty.Create(nameof(ResizeableDesktopBasePage.ParentWindowMinHeight), typeof(double), typeof(ResizeableDesktopBasePage), 
            (double)0, BindingMode.Default);

        public static readonly BindableProperty ParentWindowMinWidthProperty = 
            BindableProperty.Create(nameof(ResizeableDesktopBasePage.ParentWindowMinWidth), typeof(double), typeof(ResizeableDesktopBasePage), 
            (double)0, BindingMode.Default);
        
        public static readonly BindableProperty ParentWindowMaxHeightProperty = 
            BindableProperty.Create(nameof(ResizeableDesktopBasePage.ParentWindowMaxHeight), typeof(double), typeof(ResizeableDesktopBasePage), 
            double.PositiveInfinity, BindingMode.Default);

        public static readonly BindableProperty ParentWindowMaxWidthProperty = 
            BindableProperty.Create(nameof(ResizeableDesktopBasePage.ParentWindowMaxWidth), typeof(double), typeof(ResizeableDesktopBasePage), 
            double.PositiveInfinity, BindingMode.Default);
        
        
        public static readonly BindableProperty ParentWindowAllowResizeProperty = 
            BindableProperty.Create(nameof(ResizeableDesktopBasePage.ParentWindowAllowResize), typeof(bool), typeof(ResizeableDesktopBasePage),
            true, BindingMode.TwoWay, propertyChanged: OnAllowResizePropertyChanged);

        private double _lastKnownParentWindowHeight;
        private double _lastKnownParentWindowWidth;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.Window is PrimaryWindow primaryWindow)
                SizeAsPrimaryWindow();
            else if (this.Window is SecondaryWindow secondaryWindow)
                SizeAsSecondaryWindow();
            
            if (this.ParentWindowAllowResize)
                Task.Run(async () => await SetDefaultWindowValuesAsync());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            if (this.Window == null)
                return;
            
            _lastKnownParentWindowHeight = this.Window.Height;
            _lastKnownParentWindowWidth = this.Window.Width;
            
            Preferences.Default.Set(Constants.SettingsLastKnownPrimaryWindowHeight, _lastKnownParentWindowHeight);
            Preferences.Default.Set(Constants.SettingsLastKnownPrimaryWindowWidth, _lastKnownParentWindowWidth);
        }


        private void SizeAsPrimaryWindow()
        {
            if (this.Window == null)
                return;

            _lastKnownParentWindowHeight = Preferences.Default.Get(Constants.SettingsLastKnownPrimaryWindowHeight, double.PositiveInfinity);
            _lastKnownParentWindowWidth = Preferences.Default.Get(Constants.SettingsLastKnownPrimaryWindowWidth, double.PositiveInfinity);

            if (!double.IsPositiveInfinity(_lastKnownParentWindowHeight) &&
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                _lastKnownParentWindowHeight != this.ParentWindowHeight)
            {
                this.Window.MinimumHeight = _lastKnownParentWindowHeight;
                this.Window.MaximumHeight = _lastKnownParentWindowHeight;
            }
            else
            {
                this.Window.MinimumHeight = double.IsPositiveInfinity(this.ParentWindowHeight) ? 0 : this.ParentWindowHeight;
                this.Window.MaximumHeight = double.IsPositiveInfinity(this.ParentWindowHeight) ? double.PositiveInfinity : this.ParentWindowHeight;
            }

            if (!double.IsPositiveInfinity(_lastKnownParentWindowWidth) &&
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                _lastKnownParentWindowWidth != this.ParentWindowWidth)
            {
                this.Window.MinimumWidth = _lastKnownParentWindowWidth;
                this.Window.MaximumWidth = _lastKnownParentWindowWidth;
            }
            else
            {
                this.Window.MinimumWidth = double.IsPositiveInfinity(this.ParentWindowWidth) ? 0 : this.ParentWindowWidth;
                this.Window.MaximumWidth = double.IsPositiveInfinity(this.ParentWindowWidth) ? double.PositiveInfinity : this.ParentWindowWidth;
            }
        }

        private void SizeAsSecondaryWindow()
        {
            if (this.Window == null)
                return;
            
            this.Window.MinimumHeight = double.IsPositiveInfinity(this.ParentWindowHeight) ? 0 : this.ParentWindowHeight;
            this.Window.MaximumHeight = double.IsPositiveInfinity(this.ParentWindowHeight) ? double.PositiveInfinity : this.ParentWindowHeight;
            this.Window.MinimumWidth = double.IsPositiveInfinity(this.ParentWindowWidth) ? 0 : this.ParentWindowWidth;
            this.Window.MaximumWidth = double.IsPositiveInfinity(this.ParentWindowWidth) ? double.PositiveInfinity : this.ParentWindowWidth;
            
        }

        private static void OnDesiredWindowHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ResizeableDesktopBasePage current)
            {
                if (current.Window == null)
                    return;
                
                current.Window.MinimumHeight = (double)newValue;
                current.Window.MaximumHeight = (double)newValue;
            }
        }
        
        private static void OnDesiredWindowWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ResizeableDesktopBasePage current)
            {
                if (current.Window == null)
                    return;
                
                current.Window.MinimumWidth = (double)newValue;
                current.Window.MaximumWidth = (double)newValue;
            }
        }
        
        private static void OnAllowResizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ResizeableDesktopBasePage current)
            {
                if (current.Window == null)
                    return;

                if ((bool)newValue)
                {
                    Task.Run(async () => await current.SetDefaultWindowValuesAsync());
                }
                else
                {
                    current.Window.MaximumWidth = current.ParentWindowWidth;
                    current.Window.MaximumHeight = current.ParentWindowHeight;
                }
            }
        }

        public async Task SetDefaultWindowValuesAsync() =>
            await this.Dispatcher.DispatchAsync(() =>
            {
                Window window = this.Window;
                if (window == null)
                    return;

                window.MinimumWidth = this.ParentWindowMinWidth;
                window.MinimumHeight = this.ParentWindowMinHeight;
                window.MaximumWidth = this.ParentWindowMaxWidth;
                window.MaximumHeight = this.ParentWindowMaxHeight;
            });

        public double ParentWindowHeight
        {
            get => (double)GetValue(ParentWindowHeightProperty);
            set => SetValue(ParentWindowHeightProperty, value);
        }

        public double ParentWindowWidth
        {
            get => (double)GetValue(ParentWindowWidthProperty);
            set => SetValue(ParentWindowWidthProperty, value);
        }
        
        public double ParentWindowMinHeight
        {
            get => (double)GetValue(ParentWindowMinHeightProperty);
            set => SetValue(ParentWindowMinHeightProperty, value);
        }

        public double ParentWindowMinWidth
        {
            get => (double)GetValue(ParentWindowMinWidthProperty);
            set => SetValue(ParentWindowMinWidthProperty, value);
        }
        
        public double ParentWindowMaxHeight
        {
            get => (double)GetValue(ParentWindowMaxHeightProperty);
            set => SetValue(ParentWindowMaxHeightProperty, value);
        }

        public double ParentWindowMaxWidth
        {
            get => (double)GetValue(ParentWindowMaxWidthProperty);
            set => SetValue(ParentWindowMaxWidthProperty, value);
        }

        public bool ParentWindowAllowResize
        {
            get => (bool)GetValue(ParentWindowAllowResizeProperty); 
            set => SetValue(ParentWindowAllowResizeProperty, value);
        }
    }
}
