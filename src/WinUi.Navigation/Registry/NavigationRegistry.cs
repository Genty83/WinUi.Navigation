using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using WinUi.Navigation.Builders;
using WinUi.Navigation.Discovery;
using WinUi.Navigation.Extensions;
using WinUi.Navigation.Models;
using WinUi.Navigation.Options;
using WinUi.Navigation.Services;

namespace WinUi.Navigation.Registry
{
    /// <summary>
    /// Coordinates discovery, building, and runtime navigation into a unified framework.
    /// </summary>
    public sealed class NavigationRegistry : INavigationRegistry
    {
        private static NavigationRegistry? _instance;
        private readonly NavigationOptions? _options;

        public static NavigationRegistry Instance => _instance ??= new NavigationRegistry();

        public static void Configure(NavigationOptions options) =>
            _instance = new NavigationRegistry(options);

        public IReadOnlyList<object> MainItems { get; }
        public IReadOnlyList<object> FooterItems { get; }
        public INavigationService Service { get; }
        public NavigationModel? FirstPage { get; }

        public NavigationRegistry(NavigationOptions? options = null)
        {
            _options = options;

            // 1. Discover pages
            var discovered = NavigationDiscovery.Discover(options);

            // 2. Create navigation service
            Service = new NavigationService(discovered);

            // 3. Build NavigationView items
            var result = new NavigationBuilder(Service).Build(discovered);
            MainItems = result.MainItems;
            FooterItems = result.FooterItems;

            // 4. Determine default page
            FirstPage = discovered
                .Where(x => !x.IsFooterItem && x.ParentId is null)
                .OrderBy(x => x.Order)
                .FirstOrDefault();
        }

        public void AttachFrame(Frame frame) =>
            Service.AttachFrame(frame);

        public void ConnectNavigationView(NavigationView nav)
        {
            // Bind items
            nav.MenuItemsSource = MainItems;
            nav.FooterMenuItemsSource = FooterItems;

            // Wire navigation
            Service.Connect(nav);

            // 🔥 Sync NavigationView selection on ANY navigation (forward/back)
            Service.Navigated += (_, __) => SyncSelectedItem(nav);

            // Optional search
            if (_options?.SearchableMenu == true)
                new NavigationSearchExtension(Service, MainItems).Attach(nav);

            // Select + navigate to first page
            if (FirstPage is null)
                return;

            var selected = MainItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(i => (string?)i.Tag == FirstPage.Id);

            if (selected is not null)
                nav.SelectedItem = selected;

            Service.Navigate(FirstPage.Id);
        }

        /// <summary>
        /// Updates the NavigationView selection to match the current page.
        /// Called automatically on every navigation event.
        /// </summary>
        private void SyncSelectedItem(NavigationView nav)
        {
            var id = Service.CurrentPageId;
            if (id is null)
                return;

            var match = MainItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(i => (string?)i.Tag == id);

            if (match is not null)
                nav.SelectedItem = match;
        }
    }
}