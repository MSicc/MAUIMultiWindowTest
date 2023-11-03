namespace MauiMultiWindowTest.Windows
{
    public class SecondaryWindow : Window
    {
        public string? Key { get; private set; }

        public SecondaryWindow(Page page, string? title, string? key) : base(page)
        {
            ArgumentNullException.ThrowIfNull(page);
            
            this.Key = (string.IsNullOrWhiteSpace(key)) ?
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key)) : key;

            this.Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Value cannot be null or whitespace.", nameof(title)) : title;
        }
        
    }
}
