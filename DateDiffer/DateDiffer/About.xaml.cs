using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DateDiffer
{
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();
        }

        private async void BtnReview_OnClick(object sender, RoutedEventArgs e)
        {
            await Edi.UWP.Helpers.Tasks.OpenStoreReviewAsync();
        }
    }
}
