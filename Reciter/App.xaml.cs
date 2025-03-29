using Microsoft.Extensions.DependencyInjection;
using Reciter.Provider;
using Reciter.Services;
using Reciter.ViewModels;
using Reciter.Views.Pages;
using System.Configuration;
using System.Data;
using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Appearance;

namespace Reciter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        public new static App Current => (App)Application.Current;

        private static IServiceProvider Services;


        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<INavigationViewPageProvider, DependencyInjectionNavigationViewPageProvider>();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<IDataServices, XmlDataSerivce>((sp) => new XmlDataSerivce(System.AppDomain.CurrentDomain.BaseDirectory + "data.xml"));

            services.AddSingleton<IContentDialogService, ContentDialogService>();

            services.AddSingleton<MainWindow>();

            services.AddTransient<HomePage>();
            
            services.AddSingleton<WordBookPageVM>();
            services.AddSingleton<WordBookPage>();
            services.AddTransient<SpellPage>();

            return services.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = Services.GetService<MainWindow>();
            mainWindow!.Show();
        }
    }

}
