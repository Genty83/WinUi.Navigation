using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using WinUi.Navigation.Models;

namespace WinUi.Navigation.Services
{
    /// <summary>
    /// Provides runtime navigation functionality for WinUI applications.
    /// Handles navigation by page identifier, maintains a back stack,
    /// exposes breadcrumb information, and integrates with a NavigationView.
    /// </summary>
    public sealed class NavigationService : INavigationService
    {
        private Frame? _frame;
        private readonly Dictionary<string, NavigationModel> _lookup;
        private readonly Stack<string> _history = new();

        /// <summary>
        /// Raised whenever navigation completes (forward or backward).
        /// Used by NavigationRegistry to sync NavigationView selection.
        /// </summary>
        public event EventHandler? Navigated;

        /// <inheritdoc />
        public NavigationModel? Current { get; private set; }

        /// <summary>
        /// Gets the ID of the current page.
        /// </summary>
        public string? CurrentPageId => Current?.Id;

        /// <inheritdoc />
        public bool CanGoBack => _history.Count > 0;

        public NavigationService(IEnumerable<NavigationModel> items)
        {
            _lookup = items.ToDictionary(x => x.Id, x => x);
        }

        public void AttachFrame(Frame frame)
        {
            _frame = frame;
        }

        public void Navigate(string id)
        {
            if (_frame == null)
                return;

            if (!_lookup.TryGetValue(id, out var model))
                return;

            // Push current page onto history
            if (Current != null)
                _history.Push(Current.Id);

            // Navigate
            Current = model;
            _frame.Navigate(model.PageType);

            Navigated?.Invoke(this, EventArgs.Empty);
        }

        public void GoBack()
        {
            if (!CanGoBack || _frame == null)
                return;

            var previousId = _history.Pop();

            // Navigate without pushing onto history again
            if (_lookup.TryGetValue(previousId, out var model))
            {
                Current = model;
                _frame.Navigate(model.PageType);

                Navigated?.Invoke(this, EventArgs.Empty);
            }
        }

        public IReadOnlyList<NavigationModel> GetBreadcrumbs(string id)
        {
            var trail = new List<NavigationModel>();

            if (!_lookup.TryGetValue(id, out var current))
                return trail;

            while (current != null)
            {
                trail.Add(current);

                if (current.ParentId == null ||
                    !_lookup.TryGetValue(current.ParentId, out current))
                {
                    break;
                }
            }

            trail.Reverse();
            return trail;
        }

        public void Connect(NavigationView nav)
        {
            nav.SelectionChanged += (s, e) =>
            {
                if (e.SelectedItem is NavigationViewItem item &&
                    item.Tag is string id &&
                    id != CurrentPageId)
                {
                    Navigate(id);
                }
            };
        }
    }
}