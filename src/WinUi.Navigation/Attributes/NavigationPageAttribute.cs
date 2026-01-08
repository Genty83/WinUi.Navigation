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
    /// Initializes a new instance of the <see cref="NavigationPageAttribute"/> class.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class NavigationPageAttribute(
        string id,
        string title,
        Symbol icon = Symbol.Page,   // ✔ default icon
        int order = 0,
        string? parentId = null,
        string? headerText = null,
        bool isFooterItem = false,
        bool separatorBefore = false,
        bool separatorAfter = false) : Attribute
    {
        public string Id { get; } = id;
        public string? ParentId { get; } = parentId;
        public string Title { get; } = title;
        public Symbol Icon { get; } = icon;
        public int Order { get; } = order;
        public string? HeaderText { get; } = headerText;
        public bool IsFooterItem { get; } = isFooterItem;
        public bool SeparatorBefore { get; } = separatorBefore;
        public bool SeparatorAfter { get; } = separatorAfter;
    }
}