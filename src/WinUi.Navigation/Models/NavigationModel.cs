using System;
using Microsoft.UI.Xaml.Controls;

namespace WinUi.Navigation.Models
{
    /// <summary>
    /// Represents a single navigable page discovered by the navigation system.
    /// This model is populated from <see cref="NavigationPageAttribute"/> metadata
    /// and consumed by the navigation builder, registry, and service.
    /// </summary>
    public sealed class NavigationModel
    {
        /// <summary>
        /// Gets the unique identifier for this page.
        /// This value is used internally for navigation, lookup, and hierarchy.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// Gets the identifier of the parent page, if any.
        /// When set, this page will be nested under its parent in the NavigationView.
        /// </summary>
        public string? ParentId { get; init; }

        /// <summary>
        /// Gets the display title shown in the NavigationView.
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// Gets the icon displayed alongside the title in the NavigationView.
        /// </summary>
        public Symbol Icon { get; init; }

        /// <summary>
        /// Gets the ordering value used to sort pages within the same parent group.
        /// Lower values appear earlier in the navigation list.
        /// </summary>
        public int Order { get; init; }

        /// <summary>
        /// Gets optional header text that appears above this item in the NavigationView.
        /// Useful for grouping related pages.
        /// </summary>
        public string? HeaderText { get; init; }

        /// <summary>
        /// Gets a value indicating whether this page should appear in the footer section
        /// of the NavigationView instead of the main menu.
        /// </summary>
        public bool IsFooterItem { get; init; }

        /// <summary>
        /// Gets a value indicating whether a separator should be inserted before this item.
        /// </summary>
        public bool SeparatorBefore { get; init; }

        /// <summary>
        /// Gets a value indicating whether a separator should be inserted after this item.
        /// </summary>
        public bool SeparatorAfter { get; init; }

        /// <summary>
        /// Gets the actual WinUI <see cref="Type"/> of the page.
        /// This is used by <see cref="NavigationService"/> to perform navigation.
        /// </summary>
        public Type PageType { get; init; } = default!;
    }
}