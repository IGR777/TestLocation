using System;
using Newtonsoft.Json;
using System.Diagnostics;

namespace TestLocation
{
	public class WeatherDTO
	{

		[JsonProperty(PropertyName = "main/temp")]   
		public int Temperature { get; set; }

		[JsonProperty(PropertyName = "weather/main")]   
		public string WeatherCondition { get; set; }

		public WeatherEntity DtoToEntity(){
			var result = new WeatherEntity ();
			result.Temperature = Temperature;
			WeatherCondition condition;
			Enum.TryParse<WeatherCondition> (WeatherCondition, out condition);
			result.WeatherCondition = condition;
			if (result.WeatherCondition.Equals (default(WeatherCondition))) {
				Debug.WriteLine ("Unknow switch - {0}", WeatherCondition);
			}
			return result;
		}
	}
}

