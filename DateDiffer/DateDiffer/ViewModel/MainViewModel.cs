using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;

namespace DateDiffer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public Task TimerTask { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, o) =>
            {
                CurrentTime = DateTime.Now;
            };

            timer.Start();
        }

        private DateTime _currentTime;

        public DateTime CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; RaisePropertyChanged(); }
        }
    }
}