using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using WinUi.Navigation.Models;
using WinUi.Navigation.Services;

namespace WinUi.Navigation.Registry
{
    /// <summary>
    /// Defines the contract for the navigation registry, which coordinates discovery,
    /// building, and runtime navigation services into a unified navigation system.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="INavigationRegistry"/> interface represents the central entry point
    /// for the WinUi.Navigation framework. It exposes the navigation structure (main and
    /// footer items), the runtime navigation service, and helper methods for attaching
    /// UI components such as the <see cref="Frame"/> and <see cref="NavigationView"/>.
    /// </para>
    ///
    /// <para>
    /// Implementations of this interface are responsible for:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Discovering pages decorated with <see cref="Attributes.NavigationPageAttribute"/></description></item>
    /// <item><description>Building NavigationView items</description></item>
    /// <item><description>Providing the <see cref="INavigationService"/> instance</description></item>
    /// <item><description>Exposing the first page for default navigation</description></item>
    /// </list>
    ///
    /// <para>
    /// Applications typically register an implementation of this interface in their
    /// dependency injection container and consume it within their shell (e.g., MainWindow).
    /// </para>
    /// </remarks>
    public interface INavigationRegistry
    {
        /// <summary>
        /// Gets the collection of main navigation items to be assigned to the NavigationView.
        /// </summary>
        IReadOnlyList<object> MainItems { get; }

        /// <summary>
        /// Gets the collection of footer navigation items to be assigned to the NavigationView.
        /// </summary>
        IReadOnlyList<object> FooterItems { get; }

        /// <summary>
        /// Gets the navigation service responsible for runtime navigation behavior.
        /// </summary>
        INavigationService Service { get; }

        /// <summary>
        /// Gets the first top-level page discovered, used for default navigation.
        /// </summary>
        NavigationModel? FirstPage { get; }

        /// <summary>
        /// Attaches a <see cref="Frame"/> to the navigation service.
        /// This frame is used to perform page navigation.
        /// </summary>
        /// <param name="frame">The frame instance used for navigation.</param>
        void AttachFrame(Frame frame);

        /// <summary>
        /// Connects the navigation service to a <see cref="NavigationView"/> instance.
        /// This enables automatic navigation when items are selected.
        /// </summary>
        /// <param name="nav">The NavigationView to connect.</param>
        void ConnectNavigationView(NavigationView nav);
    }
}