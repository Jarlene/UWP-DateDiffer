using System;
using System.Globalization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace DateDiffer.ViewModel
{
    public class ConvertViewModel : ViewModelBase
    {
        public ConvertViewModel()
        {
            CommandReset = new RelayCommand(ResetConvertTextBox);
        }

        public RelayCommand CommandReset { get; set; }

        private void DoConvert(int type)
        {
            var ts = new TimeSpan(Day, Hour, Minute, Second);

            if (Month > 0)
            {
                // regards as 31 days
                ts += new TimeSpan(31, 0, 0, 0);
            }

            if (Year > 0)
            {
                // regards as 365 days
                ts += new TimeSpan(365, 0, 0, 0);
            }

            switch (type)
            {
                case 1:
                    ConvertResult = ts.TotalHours.ToString(CultureInfo.InvariantCulture);
                    break;
                case 2:
                    ConvertResult = ts.TotalMinutes.ToString(CultureInfo.InvariantCulture);
                    break;
                case 3:
                    ConvertResult = ts.TotalSeconds.ToString(CultureInfo.InvariantCulture);
                    break;
            }
        }

        protected void ResetConvertTextBox()
        {
            Year = 0;
            Month = 0;
            Day = 0;
            Hour = 0;
            Minute = 0;
            Second = 0;

            ConvertResult = string.Empty;
            IsConvertToHour = false;
            IsConvertToMinute = false;
            IsConvertToSecond = false;
        }

        private bool _isConvertToHour;

        public bool IsConvertToHour
        {
            get { return _isConvertToHour; }
            set
            {
                _isConvertToHour = value; RaisePropertyChanged();
                if (value)
                {
                    DoConvert(1);
                }
            }
        }

        private bool _isConvertToMinute;

        public bool IsConvertToMinute
        {
            get { return _isConvertToMinute; }
            set
            {
                _isConvertToMinute = value; RaisePropertyChanged();
                if (value)
                {
                    DoConvert(2);
                }
            }
        }

        private bool _isConvertToSecond;

        public bool IsConvertToSecond
        {
            get { return _isConvertToSecond; }
            set
            {
                _isConvertToSecond = value; RaisePropertyChanged();
                if (value)
                {
                    DoConvert(3);
                }
            }
        }

        private string _convertResult;
        private int _day;
        private int _hour;
        private int _minute;
        private int _month;
        private int _second;
        private int _year;

        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged("Year");
            }
        }

        public int Month
        {
            get { return _month; }
            set
            {
                _month = value;
                RaisePropertyChanged("Month");
            }
        }

        public int Day
        {
            get { return _day; }
            set
            {
                _day = value;
                RaisePropertyChanged("Day");
            }
        }

        public int Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                RaisePropertyChanged("Hour");
            }
        }

        public int Minute
        {
            get { return _minute; }
            set
            {
                _minute = value;
                RaisePropertyChanged("Minute");
            }
        }

        public int Second
        {
            get { return _second; }
            set
            {
                _second = value;
                RaisePropertyChanged("Second");
            }
        }

        public string ConvertResult
        {
            get { return _convertResult; }
            set
            {
                _convertResult = value;
                RaisePropertyChanged("ConvertResult");
            }
        }

    }
}
