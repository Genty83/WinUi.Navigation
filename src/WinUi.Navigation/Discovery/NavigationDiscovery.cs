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
    /// <remarks>
    /// <para>
    /// Discovery behavior can be customized using <see cref="NavigationOptions"/>,
    /// allowing applications to restrict scanning to specific namespaces. This makes
    /// the navigation system portable across projects with different folder or
    /// namespace structures (e.g., <c>Apps/</c>, <c>Pages/</c>, <c>Views/</c>,
    /// <c>Modules/</c>, etc.).
    /// </para>
    ///
    /// <para>
    /// Only types that:
    /// </para>
    /// <list type="bullet">
    /// <item><description>derive from <see cref="Page"/></description></item>
    /// <item><description>are non-abstract</description></item>
    /// <item><description>are decorated with <see cref="NavigationPageAttribute"/></description></item>
    /// </list>
    /// <para>
    /// are included in the discovery results.
    /// </para>
    /// </remarks>
    public static class NavigationDiscovery
    {
        /// <summary>
        /// Discovers all navigable pages in the application's entry assembly,
        /// applying optional namespace filtering based on the provided options.
        /// </summary>
        /// <param name="options">
        /// The navigation options controlling discovery behavior. If <c>null</c>,
        /// all namespaces are included.
        /// </param>
        /// <returns>
        /// A list of <see cref="NavigationModel"/> instances representing all
        /// discovered pages.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if a type is decorated with <see cref="NavigationPageAttribute"/>
        /// but does not derive from <see cref="Page"/>.
        /// </exception>
        public static List<NavigationModel> Discover(NavigationOptions? options)
        {
            options ??= new NavigationOptions();

            // The entry assembly is assumed to contain the application's pages.
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
            return types
                .Select(t => (Type: t, Attr: t.GetCustomAttribute<NavigationPageAttribute>()))
                .Where(x => x.Attr != null)
                .Select(x => new NavigationModel
                {
                    Id = x.Attr!.Id,
                    ParentId = x.Attr.ParentId,
                    Title = x.Attr.Title,
                    Icon = x.Attr.Icon,
                    Order = x.Attr.Order,
                    HeaderText = x.Attr.HeaderText,
                    IsFooterItem = x.Attr.IsFooterItem,
                    SeparatorBefore = x.Attr.SeparatorBefore,
                    SeparatorAfter = x.Attr.SeparatorAfter,
                    PageType = x.Type
                })
                .OrderBy(x => x.ParentId)
                .ThenBy(x => x.Order)
                .ToList();
        }
    }
}