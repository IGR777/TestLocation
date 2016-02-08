using System;

namespace TestLocation
{
	public class LocationEventArgs : EventArgs
	{
		#region public properties
		public double Latitude { get; set; }

		public double Longtitude { get; set; }
		#endregion

		#region .ctors
		public LocationEventArgs (double latitude, double longtitude)
		{
			Latitude = latitude;
			Longtitude = longtitude;
		}
		#endregion
	}
}

