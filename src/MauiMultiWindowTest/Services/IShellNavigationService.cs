namespace MauiMultiWindowTest.Services
{

        public interface IShellNavigationService
        {
            Task NavigateToRouteAsync(string? route, bool keepNavigationStack = true, Dictionary<string, string>? parameters = null, ShellNavigationSearchDirection searchDirection = ShellNavigationSearchDirection.Down);

            Task GoBackAsync(string? route = null, Dictionary<string, string>? parameters = null);
        }
    
        public enum ShellNavigationSearchDirection
        {
            Up,
            Down
        }
            
    
}
