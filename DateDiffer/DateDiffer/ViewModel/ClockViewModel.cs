using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using DateDiffer.Models;
using Edi.UWP.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimeZones;

namespace DateDiffer.ViewModel
{
    public class ClockViewModel : ViewModelBase
    {
        private const string Jsonfilename = "data.json";

        public ClockViewModel()
        {
            CurrentUTCTime = DateTime.UtcNow;
            Items = new ObservableCollection<ClockItem>();
            InitDataTask = InitData();

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, o) =>
            {
                CurrentUTCTime = DateTime.UtcNow;
            };

            timer.Start();

            CommandSelection = new RelayCommand(ToggleSelectionMode);
            CommandSave = new RelayCommand(async () => await SaveItems());
            CommandAdd = new RelayCommand(async () => await AddItem());
            CommandDelete = new RelayCommand(async () => await DeleteItems());
        }

        #region Commands

        public RelayCommand CommandSelection { get; set; }
        public RelayCommand CommandSave { get; set; }

        public RelayCommand CommandAdd { get; set; }

        public RelayCommand CommandDelete { get; set; }

        private async Task AddItem()
        {
            if (null != SelectedTimeZone && Items.All(i => i.TimeZone.Id != SelectedTimeZone.Id))
            {
                var tz = TimeZoneService.FindSystemTimeZoneById(SelectedTimeZone.Id);
                if (null != tz)
                {
                    Items.Add(new ClockItem(tz.Id));

                    await SaveDataAsync();
                }
            }
        }

        public void NotifySelectItem(string id)
        {
            
        }

        private async Task DeleteItems()
        {
            Items = Items.Where(i => !i.IsSelected).ToObservableCollection();
            await SaveDataAsync();
        }

        private void ToggleSelectionMode()
        {
            IsInSelectionMode = !isInSelectionMode;
        }

        private async Task SaveItems()
        {
            await SaveDataAsync();
            await new MessageDialog("Saved Successfully", "Successs").ShowAsync();
        }

        #endregion

        #region Properties

        private TimeZoneSelectItem _selectedTimeZone;

        public TimeZoneSelectItem SelectedTimeZone
        {
            get { return _selectedTimeZone; }
            set { _selectedTimeZone = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TimeZoneSelectItem> _allTimeZones;

        public ObservableCollection<TimeZoneSelectItem> AllTimeZones
        {
            get
            {
                var allzones = TimeZoneService.AllTimeZones.Select(tz => tz.ToTimeZoneSelectItem());

                _allTimeZones = allzones.OrderBy(z => z.UtcOffset).ToObservableCollection();
                return _allTimeZones;
            }
        }

        private bool isInSelectionMode;

        public bool IsInSelectionMode
        {
            get { return isInSelectionMode; }
            set { isInSelectionMode = value; RaisePropertyChanged(); }
        }

        public Task TimerTask { get; set; }
        public Task InitDataTask { get; set; }

        private DateTime _currentUTCTime;

        private ObservableCollection<ClockItem> _items;

        public DateTime CurrentUTCTime
        {
            get { return _currentUTCTime; }
            set
            {
                _currentUTCTime = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ClockItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        private async Task InitData()
        {
            var clocks = await ReadDataAsync();
            if (clocks.Any())
            {
                Items.Clear();
                foreach (var clockItem in clocks)
                {
                    Items.Add(clockItem);
                }
            }
            else
            {
                Items.Add(new ClockItem(TimeZoneService.Local.Id));
            }
        }

        private async Task SaveDataAsync()
        {
            try
            {
                List<ClockItemSerializeType> data =
                    Items.Select(i => new ClockItemSerializeType { Id = i.TimeZone.Id }).ToList();
                var serializer = new DataContractJsonSerializer(typeof(List<ClockItemSerializeType>));
                using (Stream stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                    Jsonfilename,
                    CreationCollisionOption.ReplaceExisting))
                {
                    serializer.WriteObject(stream, data);
                }
            }
            catch (Exception e)
            {
                var dig = new MessageDialog(e.Message, "Error Saving Data");
                await dig.ShowAsync();
            }
        }

        private async Task<List<ClockItem>> ReadDataAsync()
        {
            Stream ms = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(Jsonfilename);
            var serializer = new DataContractJsonSerializer(typeof(List<ClockItemSerializeType>));
            object obj = serializer.ReadObject(ms);
            var ids = obj as List<ClockItemSerializeType>;
            if (ids != null)
            {
                IEnumerable<ClockItem> items = ids.Select(i => new ClockItem(i.Id));
                return items.ToList();
            }
            return new List<ClockItem>();
        }
    }
}