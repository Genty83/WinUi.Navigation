using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using WinUi.Navigation.Models;
using WinUi.Navigation.Services;

namespace WinUi.Navigation.Builders
{
    /// <summary>
    /// Builds NavigationView UI elements from a flat collection of
    /// <see cref="NavigationModel"/> instances.
    /// </summary>
    /// <remarks>
    /// The <see cref="NavigationBuilder"/> is responsible only for constructing
    /// the visual navigation structure (main items, footer items, headers,
    /// separators, and nested items). It does not perform discovery or navigation.
    ///
    /// This class is used internally by <see cref="Registry.NavigationRegistry"/>
    /// to prepare the NavigationView for display in the application's shell.
    /// </remarks>
    public sealed class NavigationBuilder
    {
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationBuilder"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service used to perform runtime navigation.
        /// Although not directly used during building, it is injected to support
        /// future extensibility (e.g., active item highlighting, permissions, etc.).
        /// </param>
        public NavigationBuilder(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Builds the NavigationView structure from a flat list of navigation models.
        /// </summary>
        public NavigationBuildResult Build(IReadOnlyList<NavigationModel> flat)
        {
            var main = new List<object>();
            var footer = new List<object>();

            // Lookup for nesting child items under parents
            var lookup = new Dictionary<string, NavigationViewItem>();

            foreach (var model in flat.OrderBy(x => x.ParentId).ThenBy(x => x.Order))
            {
                var target = model.IsFooterItem ? footer : main;

                // Optional header
                if (!string.IsNullOrWhiteSpace(model.HeaderText))
                    target.Add(new NavigationViewItemHeader { Content = model.HeaderText });

                // Optional separator before
                if (model.SeparatorBefore)
                    target.Add(new NavigationViewItemSeparator());

                // Create the navigation item
                var navItem = new NavigationViewItem
                {
                    Content = model.Title,
                    Icon = new SymbolIcon(model.Icon),
                    Tag = model.Id
                };

                // Nesting logic
                if (model.ParentId != null && lookup.TryGetValue(model.ParentId, out var parent))
                    parent.MenuItems.Add(navItem);
                else
                    target.Add(navItem);

                // Optional separator after
                if (model.SeparatorAfter)
                    target.Add(new NavigationViewItemSeparator());

                // Store for future children
                lookup[model.Id] = navItem;
            }

            return new NavigationBuildResult(main, footer);
        }
    }

    /// <summary>
    /// Represents the result of building the NavigationView structure.
    /// Contains the main and footer items to be assigned to the NavigationView.
    /// </summary>
    public sealed class NavigationBuildResult
    {
        public IReadOnlyList<object> MainItems { get; }
        public IReadOnlyList<object> FooterItems { get; }

        public NavigationBuildResult(
            IReadOnlyList<object> mainItems,
            IReadOnlyList<object> footerItems)
        {
            MainItems = mainItems;
            FooterItems = footerItems;
        }
    }
}