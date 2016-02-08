using Cirrious.MvvmCross.ViewModels;

namespace TestLocation.ViewModels
{
    public class MainViewModel 
		: MvxViewModel
    {
		int? _temperature;

		public int? Temperature {
			get {
				return _temperature;
			}
			set {
				_temperature = value;
				RaisePropertyChanged (() => Temperature);
			}
		}

		WeatherCondition _weatherCondition;

		public WeatherCondition WeatherCondition {
			get {
				return _weatherCondition;
			}
			set {
				_weatherCondition = value;
				RaisePropertyChanged (() => WeatherCondition);
			}
		}
    }
}
