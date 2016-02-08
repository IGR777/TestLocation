using System;
using MvvmCross.Plugins.Messenger;

namespace TestLocation
{
	public class WeatherChangedMessage : MvxMessage
	{
		#region public properties
		public int Temperature { get; set; }

		public WeatherCondition WeatherCondition { get; set; }
		#endregion

		#region .ctors
		public WeatherChangedMessage (object sender, int temperature, WeatherCondition weatherCondition) : base (sender)
		{
			Temperature = temperature;
			WeatherCondition = weatherCondition;
		}
		#endregion
		
	}
}

