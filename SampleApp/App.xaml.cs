using Microsoft.UI.Xaml;
using SampleApp.Shell;
using WinUi.Navigation.Options;
using WinUi.Navigation.Registry;

namespace SampleApp
{
    public partial class App : Application
    {
        private Window? _window;

        public App()
        {
            InitializeComponent();

            NavigationRegistry.Configure(new NavigationOptions
            {
                IncludeNamespaces = ["SampleApp.Apps"],
                SearchableMenu = true
            });
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {

            _window = new MainWindow();
            _window.Activate();
        }
    }
}