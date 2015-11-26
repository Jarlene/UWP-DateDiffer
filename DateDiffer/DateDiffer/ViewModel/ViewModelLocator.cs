using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace DateDiffer.ViewModel
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DiffViewModel>();
            SimpleIoc.Default.Register<ConvertViewModel>();
            SimpleIoc.Default.Register<TimeZoneViewModel>();
            SimpleIoc.Default.Register<ClockViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public DiffViewModel Diff => ServiceLocator.Current.GetInstance<DiffViewModel>();

        public ConvertViewModel Convert => ServiceLocator.Current.GetInstance<ConvertViewModel>();
        public TimeZoneViewModel TimeZone => ServiceLocator.Current.GetInstance<TimeZoneViewModel>();

        public ClockViewModel Clock => ServiceLocator.Current.GetInstance<ClockViewModel>();

        public AboutViewModel About => ServiceLocator.Current.GetInstance<AboutViewModel>();

        public static void Cleanup()
        {
        }
    }
}