using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using DateDiffer.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DateDiffer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Clock : Page
    {
        public Clock()
        {
            this.InitializeComponent();
        }

        private void BtnSelect_OnClick(object sender, RoutedEventArgs e)
        {
            TzList.SelectionMode = TzList.SelectionMode == ListViewSelectionMode.Multiple ? ListViewSelectionMode.None : ListViewSelectionMode.Multiple;
        }

        private void TzList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as ViewModel.ClockViewModel;
            if (vm != null)
            {
                Debug.WriteLine(e.AddedItems.Count);

                foreach (var vmItem in e.AddedItems.Select(item => item as ClockItem).Select(c => vm.Items.FirstOrDefault(i => c != null && i.TimeZone.Id == c.TimeZone.Id)).Where(vmItem => null != vmItem))
                {
                    Debug.WriteLine("Setting selected item " + vmItem.TimeZone.Id);
                    vmItem.IsSelected = true;
                }

                foreach (var vmItem in e.RemovedItems.Select(item => item as ClockItem).Select(c => vm.Items.FirstOrDefault(i => c != null && i.TimeZone.Id == c.TimeZone.Id)).Where(vmItem => null != vmItem))
                {
                    Debug.WriteLine("Setting unselected item " + vmItem.TimeZone.Id);
                    vmItem.IsSelected = false;
                }

                BtnDelete.IsEnabled = vm.Items.Any(i => i.IsSelected);
            }
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            AddUI.Visibility = Visibility.Visible;
        }

        private void BtnDoAdd_Click(object sender, RoutedEventArgs e)
        {
            AddUI.Visibility = Visibility.Collapsed;
        }
    }
}
