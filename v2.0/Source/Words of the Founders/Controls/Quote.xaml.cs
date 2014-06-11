using System;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using Wwfd.WordsOfTheFounders.ServiceReferences.WwfdSearchService;

namespace Words_of_the_Founders.Controls
{
	public sealed partial class QuoteControl : UserControl
	{
		public QuoteControl()
		{
			InitializeComponent();

			TextBlockFounder.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 225));
			TextBlockQuote.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 225));
			TextBlockReferenceInfo.Foreground = new SolidColorBrush(Color.FromArgb(255, 249, 249, 18));
		}

		public int QuoteId { get; set; }

		public string FounderName
		{
			get { return TextBlockFounder.Text; }
			set { TextBlockFounder.Text = value; }
		}

		public string QuoteName
		{
			get { return TextBlockQuote.Text; }
			set { TextBlockQuote.Text = value; }
		}

		public string Reference
		{
			get { return TextBlockReferenceInfo.Text; }
			set { TextBlockReferenceInfo.Text = value; }
		}

		public ImageSource ImageUrl
		{
			get { return ImageFounder.Source; }
			set { ImageFounder.Source = value; }
		}

		private void UserControl_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
		{
			switch (e.HoldingState)
			{
				case Windows.UI.Input.HoldingState.Started:
					SetHoverColors();

					break;
				case Windows.UI.Input.HoldingState.Canceled:
				case Windows.UI.Input.HoldingState.Completed:
					SetDefaultColors();
					break;
			}
		}

		private void SetDefaultColors()
		{
			GridUserControl.Background = new SolidColorBrush(Colors.Transparent);
			TextBlockFounder.Foreground = new SolidColorBrush(Colors.White);
			TextBlockQuote.Foreground = new SolidColorBrush(Colors.White);
		}

		private void SetHoverColors()
		{
			GridUserControl.Background = new SolidColorBrush(Color.FromArgb(100,255,255,255));
			TextBlockFounder.Foreground = new SolidColorBrush(Colors.Black);
			TextBlockQuote.Foreground = new SolidColorBrush(Colors.Black);
		}

		private async void GridUserControl_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
		{
			await Launcher.LaunchUriAsync(new Uri("http://whatwouldthefoundersdo.org/showQuote.aspx?q=" + QuoteId));
		}

		private void UserControl_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
		{
			SetHoverColors();
		}

		private void UserControl_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
		{
			SetDefaultColors();
		}

		private void UserControl_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
		{
			SetClickColors();
		}

		private void SetClickColors()
		{
			GridUserControl.Background = new SolidColorBrush(Colors.White);
			TextBlockFounder.Foreground = new SolidColorBrush(Colors.Black);
			TextBlockQuote.Foreground = new SolidColorBrush(Colors.Black);
		}
	}
}
