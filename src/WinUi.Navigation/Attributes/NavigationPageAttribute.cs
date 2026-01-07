using System;
using Microsoft.UI.Xaml.Controls;

namespace WinUi.Navigation.Attributes
{
    /// <summary>
    /// Declares metadata for a navigable page within a WinUI application.
    /// Apply this attribute to any <see cref="Page"/> class to make it discoverable
    /// by the WinUi.Navigation system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class NavigationPageAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationPageAttribute"/> class.
        /// </summary>
        public NavigationPageAttribute(
            string id,
            string title,
            Symbol icon = Symbol.Page,   // ✔ default icon
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

        public string Id { get; }
        public string? ParentId { get; }
        public string Title { get; }
        public Symbol Icon { get; } 
        public int Order { get; }
        public string? HeaderText { get; }
        public bool IsFooterItem { get; }
        public bool SeparatorBefore { get; }
        public bool SeparatorAfter { get; }
    }
}