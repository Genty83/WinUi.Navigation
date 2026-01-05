using Microsoft.UI.Xaml.Controls;
using System.Linq;
using WinUi.Navigation.Services;

namespace WinUi.Navigation.Extensions
{
    public sealed class NavigationSearchExtension
    {
        private readonly INavigationService _navigationService;
        private readonly IReadOnlyList<object> _originalItems;

        public NavigationSearchExtension(
            INavigationService navigationService,
            IReadOnlyList<object> mainItems)
        {
            _navigationService = navigationService;
            _originalItems = mainItems;
        }

        public void Attach(NavigationView nav)
        {
            var searchBox = new AutoSuggestBox
            {
                PlaceholderText = "Search...",
                QueryIcon = new SymbolIcon(Symbol.Find)
            };

            nav.AutoSuggestBox = searchBox;

            searchBox.TextChanged += (s, e) => OnTextChanged(nav, s, e);
            searchBox.QuerySubmitted += OnQuerySubmitted;
        }

        private void OnTextChanged(
            NavigationView nav,
            AutoSuggestBox sender,
            AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput)
                return;

            var query = sender.Text;

            if (string.IsNullOrWhiteSpace(query))
            {
                // Restore full menu
                nav.MenuItemsSource = _originalItems;
                sender.ItemsSource = null;
                return;
            }

            var matches = _originalItems
                .OfType<NavigationViewItem>()
                .Where(i => i.Content?.ToString()?.Contains(query, StringComparison.InvariantCultureIgnoreCase) == true)
                .ToList();

            // Update suggestions
            sender.ItemsSource = matches.Select(i => i.Content.ToString()).ToList();

            // Update NavigationView
            nav.MenuItemsSource = matches;
        }

        private void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var query = args.QueryText;

            if (string.IsNullOrWhiteSpace(query))
                return;

            var match = _originalItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(i => i.Content?.ToString()?.Contains(query, StringComparison.InvariantCultureIgnoreCase) == true);

            if (match is not null)
            {
                sender.ItemsSource = null;
                sender.Text = string.Empty;

                _navigationService.Navigate((string)match.Tag);
            }
        }
    }
}