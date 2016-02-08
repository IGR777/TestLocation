using System;
using MvvmCross.Platform.Converters;

namespace TestLocation.Droid
{
	public class StateValueConverter : MvxValueConverter<WeatherCondition, int>
	{
		protected override int Convert (WeatherCondition value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			switch(value){
			case WeatherCondition.Clear:
				return Resource.Drawable.sun;
			case WeatherCondition.Atmosphere:
				return Resource.Drawable.mist;
			case WeatherCondition.Clouds:
				return Resource.Drawable.sun_clouds;
			case WeatherCondition.Drizzle:
				return Resource.Drawable.clouds;
			case WeatherCondition.Extreme:
			case WeatherCondition.Thunderstorm:
				return Resource.Drawable.thunderstorm;
			case WeatherCondition.Rain:
				return Resource.Drawable.rain;
			case WeatherCondition.Snow:
				return Resource.Drawable.snow;
			case WeatherCondition.Undefined:
				return Resource.Drawable.question;			
			}
			return Resource.Drawable.question;	
		}
	}
}

