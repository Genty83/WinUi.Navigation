using System.Collections.Generic;

namespace WinUi.Navigation.Options
{
    /// <summary>
    /// Provides configuration options for the WinUi.Navigation framework.
    /// These options control how pages are discovered and how the navigation
    /// system behaves across different application structures.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="NavigationOptions"/> class allows applications to customize
    /// the behavior of the navigation system so that it can be reused across
    /// multiple projects with different folder structures, namespaces, or
    /// architectural conventions.
    /// </para>
    ///
    /// <para>
    /// The most common use case is specifying which namespaces should be scanned
    /// for pages decorated with <see cref="Attributes.NavigationPageAttribute"/>.
    /// This enables projects to organize their pages in folders such as:
    /// </para>
    ///
    /// <list type="bullet">
    /// <item><description><c>Apps/</c></description></item>
    /// <item><description><c>Pages/</c></description></item>
    /// <item><description><c>Views/</c></description></item>
    /// <item><description><c>Modules/</c></description></item>
    /// <item><description><c>Features/</c></description></item>
    /// </list>
    ///
    /// <para>
    /// If no namespaces are specified, the navigation system will scan the entire
    /// entry assembly for eligible pages.
    /// </para>
    /// </remarks>
    public sealed class NavigationOptions
    {
        /// <summary>
        /// Gets or sets the list of namespaces that should be included during
        /// page discovery.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When specified, only types whose <see cref="System.Type.Namespace"/>
        /// begins with one of the provided namespace prefixes will be considered
        /// for navigation.
        /// </para>
        ///
        /// <para>
        /// This allows applications to restrict discovery to specific modules or
        /// folders, improving performance and preventing accidental inclusion of
        /// unrelated pages.
        /// </para>
        ///
        /// <para>
        /// Example:
        /// <code>
        /// new NavigationOptions
        /// {
        ///     IncludeNamespaces = new[]
        ///     {
        ///         "MyApp.Pages",
        ///         "MyApp.Modules.Reports"
        ///     }
        /// };
        /// </code>
        /// </para>
        ///
        /// <para>
        /// If <c>null</c> or empty, all namespaces are included.
        /// </para>
        /// </remarks>
        public IReadOnlyList<string>? IncludeNamespaces { get; set; }

        /// <summary>
        /// Enables the built‑in search box inside the NavigationView.
        /// When true, the NavigationSearchExtension is automatically attached
        /// during NavigationRegistry.ConnectNavigationView.
        /// </summary>
        public bool SearchableMenu { get; set; } = false;
    }
}