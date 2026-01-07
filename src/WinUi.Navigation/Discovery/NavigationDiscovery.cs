using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.UI.Xaml.Controls;
using WinUi.Navigation.Attributes;
using WinUi.Navigation.Models;
using WinUi.Navigation.Options;

namespace WinUi.Navigation.Discovery
{
    /// <summary>
    /// Performs reflection-based discovery of navigable pages within the application.
    /// This class scans the entry assembly for types decorated with
    /// <see cref="NavigationPageAttribute"/> and produces a collection of
    /// <see cref="NavigationModel"/> instances.
    /// </summary>
    public static class NavigationDiscovery
    {
        /// <summary>
        /// Discovers all navigable pages in the application's entry assembly,
        /// applying optional namespace filtering based on the provided options.
        /// </summary>
        public static List<NavigationModel> Discover(NavigationOptions? options)
        {
            options ??= new NavigationOptions();

            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

            // Step 1: Find all Page-derived types
            var types = assembly
                .GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    typeof(Page).IsAssignableFrom(t));

            // Step 2: Apply namespace filtering if configured
            if (options.IncludeNamespaces is { Count: > 0 })
            {
                types = types.Where(t =>
                    t.Namespace != null &&
                    options.IncludeNamespaces.Any(prefix =>
                        t.Namespace.StartsWith(prefix, StringComparison.Ordinal)));
            }

            // Step 3: Extract attribute metadata and build NavigationModel instances
            return [.. types
                .Select(t => (Type: t, Attr: t.GetCustomAttribute<NavigationPageAttribute>()))
                .Where(x => x.Attr != null)
                .Select(x =>
                {
                    var attr = x.Attr!;

                    // Symbol-only icon factory
                    Func<IconElement>? iconFactory = () => new SymbolIcon(attr.Icon);

                    return new NavigationModel
                    {
                        Id = attr.Id,
                        ParentId = attr.ParentId,
                        Title = attr.Title,
                        Order = attr.Order,
                        HeaderText = attr.HeaderText,
                        IsFooterItem = attr.IsFooterItem,
                        SeparatorBefore = attr.SeparatorBefore,
                        SeparatorAfter = attr.SeparatorAfter,
                        PageType = x.Type,

                        // Symbol-only metadata
                        IconSymbol = attr.Icon,

                        // Final icon factory
                        IconFactory = iconFactory
                    };
                })
                .OrderBy(x => x.ParentId)
                .ThenBy(x => x.Order)];
        }
    }
}