using Wpf.Ui.Abstractions;

namespace Reciter.Provider
{
    public class DependencyInjectionNavigationViewPageProvider(IServiceProvider serviceProvider)
    : INavigationViewPageProvider
    {
        public object? GetPage(Type pageType)
        {
            return serviceProvider.GetService(pageType);
        }
    }
}
