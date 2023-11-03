using MauiMultiWindowTest.Common;
namespace MauiMultiWindowTest.Windows
{
    public class PrimaryWindow : Window
    {
        public PrimaryWindow(Page page) : base(page)
        {
            this.SizeChanged += (sender, args) =>
            {
                Console.WriteLine($"w:{this.Width} - h:{this.Height}");
            };
        }
        
        protected override void OnDestroying()
        {
            base.OnDestroying();

            if (Application.Current is null)
                return;
            
            if (Application.Current.Windows.Count == 1)
                return;

             //make sure we have only the PrimaryWindow when the application goes to background or gets closed
            foreach (Window secondaryWindow in Application.Current.Windows.Skip(1))
            {
                Application.Current.CloseWindow(secondaryWindow);
            }
        }

        protected override void OnBackgrounding(IPersistedState state)
        {
            //this never gets reloaded from the OS (or MAUI)... 
            //leaving it here for demonstrating purposes
            //needs more investigation
            state.Add(DateTime.Now.ToLongDateString(), "Went To background...");
            
            base.OnBackgrounding(state);
        }

        // ReSharper disable RedundantOverriddenMember
        // for debugging purposes
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        protected override void OnCreated()
        {
            base.OnCreated();
            

        }

        protected override void OnResumed()
        {
            base.OnResumed();
        }
        
        protected override void OnActivated()
        {
            base.OnActivated();
            
        }

    }
}
