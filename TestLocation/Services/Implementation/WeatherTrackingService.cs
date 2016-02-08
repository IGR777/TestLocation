using MvvmCross.Plugins.Messenger;


namespace TestLocation
{
	public class WeatherTrackingService : IWeatherTrackingService
	{

		#region private fields
		readonly ILocationTrackingService _locationTrackingService;
		readonly IRestService _restService;
		readonly IMvxMessenger _messenger;
		WeatherEntity _cachedWeather;
		#endregion

		#region .ctors
		public WeatherTrackingService(
			ILocationTrackingService locationTrackingService,
			IRestService restService,
			IMvxMessenger messenger)
		{
			_locationTrackingService = locationTrackingService;
			_restService = restService;
			_messenger = messenger;
		}
		#endregion

		#region IWeatherTrackingService implementation
		public void StartTracking ()
		{
			_locationTrackingService.LocationChanged += HandleLocationChanged;
			_locationTrackingService.StartTrackingUserLocation ();

		}

		public void StopTracking ()
		{
			_locationTrackingService.StopTrackingUserLocation ();
		}
		#endregion

		#region utility functions

		async void HandleLocationChanged(object sender, LocationEventArgs e){
			var weather = await _restService.GetWeather (e.Longtitude, e.Latitude);
			if (_cachedWeather == null || (_cachedWeather.Temperature != weather.Temperature || _cachedWeather.WeatherCondition != weather.WeatherCondition)) {
				_cachedWeather = weather;
				_messenger.Publish (new WeatherChangedMessage (this, weather.Temperature, weather.WeatherCondition));
			}
		}
		#endregion
		
	}
}

