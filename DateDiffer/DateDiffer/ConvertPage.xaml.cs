using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DateDiffer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertPage : Page
    {
        public ConvertPage()
        {
            this.InitializeComponent();
        }

        private void Year_Focus(object sender, RoutedEventArgs e)
        {
            TextYear.SelectAll();
        }

        private void Month_Focus(object sender, RoutedEventArgs e)
        {
            TextMonth.SelectAll();
        }

        private void Day_Focus(object sender, RoutedEventArgs e)
        {
            TextDay.SelectAll();
        }

        private void Hour_Focus(object sender, RoutedEventArgs e)
        {
            TextHour.SelectAll();
        }

        private void Min_Focus(object sender, RoutedEventArgs e)
        {
            TextMin.SelectAll();
        }

        private void Second_Focus(object sender, RoutedEventArgs e)
        {
            TextSecond.SelectAll();
        }
    }
}
