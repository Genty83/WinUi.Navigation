using System;

using WinUi.Navigation.Attributes;
using Microsoft.UI.Xaml.Controls;



namespace SampleApp.Apps.HomeApp.Views
{
    [NavigationPage(
        id: "home_page",
        title: "Home",
        icon: Symbol.Home,
        order: 0,
        separatorAfter: true,
        headerText: "General Pages"
        )]
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }
}
