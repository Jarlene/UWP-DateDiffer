using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using TimeZones;

namespace DateDiffer.Models
{
    public class ClockItem : INotifyPropertyChanged
    {
        public bool IsSelected { get; set; }

        public TimeZoneSelectItem TimeZone { get; set; }

        public DateTime Time { get; set; }

        private ITimeZoneEx _tz;

        public ClockItem(string tzoneId)
        {
            _tz = TimeZoneService.FindSystemTimeZoneById(tzoneId);
            TimeZone = _tz.ToTimeZoneSelectItem();
            //TimerTask = new AsyncTimer(1000).WhenTick(i => OnOneSecondPassed()).StartAsync();

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, o) =>
            {
                var offset = _tz.ConvertTime(DateTime.Now);
                Time = offset.DateTime;
                OnPropertyChanged("Time");
            };

            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [DataContract]
    public class ClockItemSerializeType
    {
        [DataMember]
        public string Id { get; set; }
    }
}
