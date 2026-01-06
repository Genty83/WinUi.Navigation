using WinUi.Navigation.Attributes;
using Microsoft.UI.Xaml.Controls;


namespace SampleApp.Apps.ProjectsApp.Views
{
    [NavigationPage(
        id: "NewProjectPage",
        parentId: "ProjectsPage",
        title: "New Project",
        icon: Symbol.Add,
        order: 0
        )]
    public sealed partial class NewProjectPage : Page
    {
        public NewProjectPage()
        {
            InitializeComponent();
        }
    }
}
