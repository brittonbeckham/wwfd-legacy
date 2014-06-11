using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Words_of_the_Founders.Controls;
using Wwfd.WordsOfTheFounders.ServiceReferences.WwfdSearchService;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Words_of_the_Founders
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();

			SettingsPane.GetForCurrentView().CommandsRequested += MainPage_CommandsRequested;
		}

		void MainPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			args.Request.ApplicationCommands.Clear();

			SettingsCommand termsOfUse = new SettingsCommand(
				"TermsOfUse",
				"Terms of Use",
				uiCommand =>
				{
					MessageDialog msgDialog = new MessageDialog("This app connects to a web service through the internet to retrieve its data. " +
					                                            "No personal information of any kind is accessed, transmitted, saved, stored, " +
					                                            "or otherwise used in any way by this application.", "Terms of Use");
					msgDialog.DefaultCommandIndex = 1;

					//Show message
					msgDialog.ShowAsync();
				});

			args.Request.ApplicationCommands.Add(termsOfUse);
		}

		private void ShowTermsOfUse()
		{
			
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.  The Parameter
		/// property is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			TextBoxSearch.Focus(FocusState.Keyboard);
		}

		private void TextBoxSearch_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (TextBoxSearch.Text == string.Empty && (e.Key != VirtualKey.Up && e.Key != VirtualKey.Down && e.Key != VirtualKey.Left && e.Key != VirtualKey.Right))
				TextBlockSearchPlaceHolder.Visibility = Visibility.Collapsed;

			if (e.Key == VirtualKey.Enter)
				SearchQuotes(TextBoxSearch.Text);
		}


		private async void SearchQuotes(string searchText)
		{
			GridSearchResults.Children.Clear();
			GridSearchResults.RowDefinitions.Clear();
			GridSearchResults.ColumnDefinitions.Clear();

			GridSearchResults.ColumnDefinitions.Add(new ColumnDefinition());

			WwfdSearchServiceClient client = new WwfdSearchServiceClient();

			ObservableCollection<Quote> results = await client.SearchQuotesAsync(TextBoxSearch.Text);

			if (results.Count == 0)
				return;

			int counter = 0;
			foreach (Quote q in results)
			{
				QuoteControl qc = new QuoteControl();
				qc.FounderName = q.FounderName;
				qc.QuoteName = q.QuoteText;
				qc.Reference = "Reference: " + q.ReferenceInfo;
				qc.ImageUrl = new BitmapImage(q.FounderImageThumbnail);
				qc.QuoteId = q.QuoteId;
				GridSearchResults.RowDefinitions.Add(new RowDefinition());

				GridSearchResults.Children.Add(qc);
				Grid.SetColumn(qc, 0);
				Grid.SetRow(qc, counter);

				counter++;
			}
		}

		private void TextBoxSearch_KeyUp(object sender, KeyRoutedEventArgs e)
		{
			TextBlockSearchPlaceHolder.Visibility = TextBoxSearch.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;

		}
	}
}
