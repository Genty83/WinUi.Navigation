using System;
using Microsoft.UI.Xaml.Controls;

namespace WinUi.Navigation.Attributes
{
    /// <summary>
    /// Declares metadata for a navigable page within a WinUI application.
    /// Apply this attribute to any <see cref="Page"/> class to make it discoverable
    /// by the WinUi.Navigation system.
    /// </summary>
    /// <remarks>
    /// The navigation system uses this attribute during discovery to build a
    /// structured navigation hierarchy, generate NavigationView items, and
    /// enable navigation by page identifier.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class NavigationPageAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationPageAttribute"/> class.
        /// </summary>
        /// <param name="id">
        /// A unique identifier for the page. This value is used internally for navigation,
        /// lookup, and hierarchy. It must be unique across the application.
        /// </param>
        /// <param name="title">
        /// The display title shown in the NavigationView.
        /// </param>
        /// <param name="icon">
        /// The <see cref="Symbol"/> icon displayed alongside the title in the NavigationView.
        /// </param>
        /// <param name="order">
        /// Determines the ordering of pages within the same parent group.
        /// Lower values appear earlier in the navigation list.
        /// </param>
        /// <param name="parentId">
        /// Optional identifier of the parent page. When provided, this page will be nested
        /// under its parent in the NavigationView.
        /// </param>
        /// <param name="headerText">
        /// Optional header text displayed above this item in the NavigationView.
        /// Useful for grouping related pages.
        /// </param>
        /// <param name="isFooterItem">
        /// Indicates whether this page should appear in the footer section of the NavigationView
        /// instead of the main menu.
        /// </param>
        /// <param name="separatorBefore">
        /// When true, inserts a separator before this item in the NavigationView.
        /// </param>
        /// <param name="separatorAfter">
        /// When true, inserts a separator after this item in the NavigationView.
        /// </param>
        public NavigationPageAttribute(
            string id,
            string title,
            Symbol icon,
            int order = 0,
            string? parentId = null,
            string? headerText = null,
            bool isFooterItem = false,
            bool separatorBefore = false,
            bool separatorAfter = false)
        {
            Id = id;
            Title = title;
            Icon = icon;
            Order = order;
            ParentId = parentId;
            HeaderText = headerText;
            IsFooterItem = isFooterItem;
            SeparatorBefore = separatorBefore;
            SeparatorAfter = separatorAfter;
        }

        /// <summary>
        /// Gets the unique identifier for this page.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the identifier of the parent page, if any.
        /// </summary>
        public string? ParentId { get; }

        /// <summary>
        /// Gets the display title shown in the NavigationView.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the icon displayed alongside the title in the NavigationView.
        /// </summary>
        public Symbol Icon { get; }

        /// <summary>
        /// Gets the ordering value used to sort pages within the same parent group.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Gets optional header text that appears above this item in the NavigationView.
        /// </summary>
        public string? HeaderText { get; }

        /// <summary>
        /// Gets a value indicating whether this page should appear in the footer section
        /// of the NavigationView.
        /// </summary>
        public bool IsFooterItem { get; }

        /// <summary>
        /// Gets a value indicating whether a separator should be inserted before this item.
        /// </summary>
        public bool SeparatorBefore { get; }

        /// <summary>
        /// Gets a value indicating whether a separator should be inserted after this item.
        /// </summary>
        public bool SeparatorAfter { get; }
    }
}