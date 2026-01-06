using WinUi.Navigation.Attributes;
using Microsoft.UI.Xaml.Controls;

namespace SampleApp.Apps.ProjectsApp.Views
{
    [NavigationPage(
        id: "ProjectsPage",
        title: "Projects",
        icon: Symbol.NewFolder,
        order: 2
        )]
    public sealed partial class ProjectsPage : Page
    {
        public ProjectsPage()
        {
            InitializeComponent();
        }
    }
}
