using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace DateDiffer.ViewModel
{
    public class DiffViewModel : ViewModelBase
    {
        public DiffViewModel()
        {
            StartDate = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;

            EndDate = DateTime.Now.AddDays(1);
            EndTime = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 10, 0));

            CommandGetDiff = new RelayCommand(GetDiff);
            CommandToDays = new RelayCommand(ToDays);
            CommandToHours = new RelayCommand(ToHours);
            CommandToMinutes = new RelayCommand(ToMinutes);
            CommandToSeconds = new RelayCommand(ToSeconds);
        }


        public RelayCommand CommandGetDiff { get; set; }
        public RelayCommand CommandToDays { get; set; }
        public RelayCommand CommandToHours { get; set; }
        public RelayCommand CommandToMinutes { get; set; }
        public RelayCommand CommandToSeconds { get; set; }

        private void GetDiff()
        {
            TimeSpan ts = GetTimeSpan();

            string dateDiff = ts.Days + " Day(s) " +
                              ts.Hours + " Hour(s) " +
                              ts.Minutes + " Minute(s) " +
                              ts.Seconds + " Second(s) ";

            DiffResult = dateDiff;
        }

        private void ToDays()
        {
            GetDateDiffIn("d");
        }
        private void ToHours()
        {
            GetDateDiffIn("hh");
        }

        private void ToMinutes()
        {
            GetDateDiffIn("mm");
        }

        private void ToSeconds()
        {
            GetDateDiffIn("ss");
        }

        protected void GetDateDiffIn(string datePart)
        {
            TimeSpan ts = GetTimeSpan();

            switch (datePart)
            {
                case "d":
                    DiffResult = "Total " + ts.TotalDays + " Day(s)";
                    break;
                case "hh":
                    DiffResult = "Total " + ts.TotalHours + " Hours(s)";
                    break;
                case "mm":
                    DiffResult = "Total " + ts.TotalMinutes + " Minute(s)";
                    break;
                case "ss":
                    DiffResult = "Total " + ts.TotalSeconds + " Second(s)";
                    break;
                default:
                    DiffResult = string.Empty;
                    break;
            }
        }

        private TimeSpan GetTimeSpan()
        {
            var dt1 = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hours,
                StartTime.Minutes, 0);

            var dt2 = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hours,
                EndTime.Minutes, 0);

            TimeSpan ts = GetDateDiff(dt1, dt2);
            return ts;
        }

        private static TimeSpan GetDateDiff(DateTime dateTime1, DateTime dateTime2)
        {
            var ts1 = new TimeSpan(dateTime1.Ticks);
            var ts2 = new TimeSpan(dateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }

        #region Binding Properties

        private DateTime _startDate;

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                _startDate = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _startTime;

        public TimeSpan StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }


        private DateTime _endDate;

        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }

            set
            {
                _endDate = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _endTime;

        public TimeSpan EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
                RaisePropertyChanged();
            }
        }

        private string _diffResult = "...";

        public string DiffResult
        {
            get
            {
                return _diffResult;
            }

            set
            {
                _diffResult = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}