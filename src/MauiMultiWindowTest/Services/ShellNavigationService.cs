using MauiMultiWindowTest.Common;
using Microsoft.Extensions.Logging;
namespace MauiMultiWindowTest.Services
{
    // docs: https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-7.0
    public class ShellNavigationService: IShellNavigationService
    {
        private readonly ILogger<IShellNavigationService> _logger;

        public ShellNavigationService(ILogger<ShellNavigationService> logger) =>
            _logger = logger;

        public async Task NavigateToRouteAsync(string? route, bool keepNavigationStack = true, Dictionary<string, string>? parameters = null, ShellNavigationSearchDirection searchDirection = ShellNavigationSearchDirection.Down)
        {
            string navigationPrefix = keepNavigationStack ? string.Empty : "//";

            if (searchDirection == ShellNavigationSearchDirection.Down)
                navigationPrefix += "/";

            route = $"{navigationPrefix}{route}";

            _logger.LogDebug("Navigation requested for route {Route}", route);

            if (parameters != null)
            {
                route.AddParametersToUri(parameters);
            }
            
            await Shell.Current.GoToAsync(route);
        }

        public async Task GoBackAsync(string? route = null, Dictionary<string, string>? parameters = null)
        {
            string navigationRoute = "..";

            if (!string.IsNullOrWhiteSpace(route))
                navigationRoute += $"/{route}";
            
            if (parameters != null)
            {
                navigationRoute.AddParametersToUri(parameters);
            }

            await Shell.Current.GoToAsync(navigationRoute);
        }
        
    }
}
