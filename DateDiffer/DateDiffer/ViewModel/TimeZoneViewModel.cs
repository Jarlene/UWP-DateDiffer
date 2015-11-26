using System;
using System.Collections.ObjectModel;
using System.Linq;
using DateDiffer.Models;
using Edi.UWP.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimeZones;

namespace DateDiffer.ViewModel
{
    public class TimeZoneViewModel : ViewModelBase
    {
        public TimeZoneViewModel()
        {
            SelectedDate = DateTime.Now;
            SelectedTime = DateTime.Now.TimeOfDay;

            _CommandGetTZoneResult = new RelayCommand(GetTZoneResult);
        }

        public RelayCommand _CommandGetTZoneResult { get; set; }

        private void GetTZoneResult()
        {
            var sourceDtm = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds); //SelectedDate.AddTicks(SelectedTime.Ticks);

            if (null != SelectedTimeZone)
            {
                var tz = TimeZoneService.FindSystemTimeZoneById(SelectedTimeZone.Id);
                if (null != tz)
                {
                    var offset = tz.ConvertTime(sourceDtm);
                    TZoneResult = offset.DateTime.ToString("yyyy-MM-dd dddd HH:mm:ss");
                }
            }
        }

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; RaisePropertyChanged(); }
        }

        private TimeSpan _selectedTime;

        public TimeSpan SelectedTime
        {
            get { return _selectedTime; }
            set { _selectedTime = value; RaisePropertyChanged(); }
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

        private TimeZoneSelectItem _selectedTimeZone;

        public TimeZoneSelectItem SelectedTimeZone
        {
            get { return _selectedTimeZone; }
            set { _selectedTimeZone = value; RaisePropertyChanged(); }
        }

        private string _tzoneResult;

        public string TZoneResult
        {
            get { return _tzoneResult; }
            set { _tzoneResult = value; RaisePropertyChanged(); }
        }
    }
}
