using System;

namespace TestLocation
{
	public interface ILocationTrackingService
	{
		event EventHandler<LocationEventArgs> LocationChanged;

		void StartTrackingUserLocation ();

		void StopTrackingUserLocation ();
	}
}

