using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUi.Navigation.Registry;
using WinUi.Navigation.Services;

namespace SampleApp.Shell
{
    public sealed partial class MainWindow : Window
    {
        private readonly INavigationService _navigation;

        public MainWindow()
        {
            InitializeComponent();
            SetupTitlebar();
            SetupNavigation();
        }

        private void SetupTitlebar()
        {
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
        }

        private void SetupNavigation()
        {
            var nav = NavigationRegistry.Instance;
            nav.AttachFrame(ContentFrame);
            nav.ConnectNavigationView(AppNavView);
        }

        private void RequestPaneToggle(TitleBar sender, object args)
        {
            AppNavView.IsPaneOpen = !AppNavView.IsPaneOpen;
        }

        private void GoBack(TitleBar sender, object args)
        {
            var nav = NavigationRegistry.Instance;
            nav.Service.GoBack();
        }
    }
}