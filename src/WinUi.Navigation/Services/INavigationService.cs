using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using WinUi.Navigation.Models;

namespace WinUi.Navigation.Services
{
    /// <summary>
    /// Defines the contract for runtime navigation behavior within a WinUI application.
    /// Implementations of this interface provide page navigation, back stack management,
    /// breadcrumb generation, and integration with a <see cref="NavigationView"/>.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Raised whenever navigation completes (forward or backward).
        /// Used by the navigation framework to synchronize UI state.
        /// </summary>
        event EventHandler? Navigated;

        /// <summary>
        /// Gets the currently active navigation model, or <c>null</c> if no page
        /// has been navigated to yet.
        /// </summary>
        NavigationModel? Current { get; }

        /// <summary>
        /// Gets the identifier of the currently active page, or <c>null</c> if
        /// no page has been navigated to yet.
        /// </summary>
        string? CurrentPageId { get; }

        /// <summary>
        /// Gets a value indicating whether the navigation service can navigate back.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Attaches a <see cref="Frame"/> to the navigation service.
        /// The frame is used to perform page navigation.
        /// </summary>
        /// <param name="frame">The frame instance used for navigation.</param>
        void AttachFrame(Frame frame);

        /// <summary>
        /// Navigates to the page associated with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the target page.</param>
        void Navigate(string id);

        /// <summary>
        /// Navigates back to the previous page in the navigation history.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Retrieves the breadcrumb trail for the specified page identifier.
        /// </summary>
        /// <param name="id">The identifier of the page for which to retrieve breadcrumbs.</param>
        /// <returns>
        /// A list of <see cref="NavigationModel"/> instances representing the breadcrumb
        /// hierarchy from the root to the specified page.
        /// </returns>
        IReadOnlyList<NavigationModel> GetBreadcrumbs(string id);

        /// <summary>
        /// Connects the navigation service to a <see cref="NavigationView"/> instance.
        /// This enables automatic navigation when items are selected.
        /// </summary>
        /// <param name="nav">The NavigationView to connect.</param>
        void Connect(NavigationView nav);
    }
}