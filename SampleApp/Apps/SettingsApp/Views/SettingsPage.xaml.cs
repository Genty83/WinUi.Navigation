using WinUi.Navigation.Attributes;
using Microsoft.UI.Xaml.Controls;


namespace SampleApp.Apps.SettingsApp.Views
{
    [NavigationPage(
        id: "settings_page",
        title: "Settings",
        icon: Symbol.Setting,
        order: 999,
        isFooterItem: true
        )]
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}
