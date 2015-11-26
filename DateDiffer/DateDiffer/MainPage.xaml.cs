using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DateDiffer
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Edi.UWP.Helpers.UI.SetWindowLaunchSize(720, 360);
            ApplyColorToTitleBar();
            Edi.UWP.Helpers.Mobile.SetWindowsMobileStatusBarColor(Color.FromArgb(255, 0, 114, 188), Colors.White);
        }

        void ApplyColorToTitleBar()
        {
            Edi.UWP.Helpers.UI.ApplyColorToTitleBar(
                Color.FromArgb(255, 0, 114, 188), 
                Colors.White, 
                Colors.LightGray, 
                Colors.Gray);

            Edi.UWP.Helpers.UI.ApplyColorToTitleButton(
                Color.FromArgb(255, 0, 114, 188), Colors.White, 
                Color.FromArgb(255, 51, 148, 208), Colors.White, 
                Color.FromArgb(255, 0, 114, 188), Colors.White, 
                Colors.LightGray, Colors.Gray);
        }

        private void GoToDiffPage(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DiffPage));
        }

        private void GoToConvertPage(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ConvertPage));
        }

        private void GoToTimeZonePage(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(TimeZonePage));
        }

        private void GoToClockPage(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Clock));
        }

        private void BtnAbout_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private async void BtnReview_OnClick(object sender, RoutedEventArgs e)
        {
            await Edi.UWP.Helpers.Tasks.OpenStoreReviewAsync();
        }
    }
}
