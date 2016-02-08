
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace TestLocation.ViewModels
{
    public class MainViewModel 
		: MvxViewModel
    {

		#region private fields
		IMvxMessenger _messenger;
		MvxSubscriptionToken _token;
		IWeatherTrackingService _weatherTrackingService;
		#endregion

		#region .ctors

		public MainViewModel (
			IMvxMessenger messenger,
			IWeatherTrackingService weatherTrackingService
		)
    	{
			_messenger = messenger;
			_weatherTrackingService = weatherTrackingService;
			WeatherCondition = WeatherCondition.Undefined;
    	}
		#endregion
    	
		#region public properties

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
		#endregion

		#region Commands

		public ICommand StartTracking {
			get {
				return new MvxCommand (() => {
					_token = _messenger.Subscribe<WeatherChangedMessage>((message) =>{
						Temperature = message.Temperature;
						WeatherCondition = message.WeatherCondition;
					});
					_weatherTrackingService.StartTracking();
				});
			}
		}

		public ICommand StopTracking {
			get {
				return new MvxCommand (() => {
					_weatherTrackingService.StopTracking();
					_messenger.Unsubscribe<WeatherChangedMessage>(_token);
				});
			}
		}
		#endregion
    }
}
