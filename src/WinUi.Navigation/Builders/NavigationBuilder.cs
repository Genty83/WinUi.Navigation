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
    public sealed class NavigationBuilder(INavigationService navigationService)
    {
        private readonly INavigationService _navigationService = navigationService;

        /// <summary>
        /// Builds the NavigationView structure from a flat list of navigation models.
        /// </summary>
        public NavigationBuildResult Build(IReadOnlyList<NavigationModel> flat)
        {
            var main = new List<object>();
            var footer = new List<object>();

            // Lookup for nesting child items under parents
            var lookup = new Dictionary<string, NavigationViewItem>();

            foreach (var model in flat
                .OrderBy(x => x.ParentId is null ? 0 : 1) // parents first
                .ThenBy(x => x.Order))
            {
                var target = model.IsFooterItem ? footer : main;

                // Optional header
                if (!string.IsNullOrWhiteSpace(model.HeaderText))
                {
                    target.Add(new NavigationViewItemHeader
                    {
                        Content = model.HeaderText
                    });
                }

                // Optional separator before
                if (model.SeparatorBefore)
                {
                    target.Add(new NavigationViewItemSeparator());
                }

                // Create the navigation item
                var navItem = new NavigationViewItem
                {
                    Content = model.Title,
                    Tag = model.Id
                };

                // Nesting logic
                if (model.ParentId is not null &&
                    lookup.TryGetValue(model.ParentId, out var parent))
                {
                    parent.MenuItems.Add(navItem);
                }
                else
                {
                    target.Add(navItem);
                }

                // Optional separator after
                if (model.SeparatorAfter)
                {
                    target.Add(new NavigationViewItemSeparator());
                }

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
    public sealed class NavigationBuildResult(
        IReadOnlyList<object> mainItems,
        IReadOnlyList<object> footerItems)
    {
        public IReadOnlyList<object> MainItems { get; } = mainItems;
        public IReadOnlyList<object> FooterItems { get; } = footerItems;
    }
}