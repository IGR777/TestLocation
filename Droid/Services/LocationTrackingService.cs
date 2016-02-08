using System;
using Android.Content;
using Android.Locations;
using System.Threading.Tasks;
using Android.OS;
using Android;
using Android.Content.PM;
using Android.Support.Design.Widget;
using System.Linq;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using Android.App;
using Android.Widget;

namespace TestLocation.Droid
{
	public class LocationTrackingService : Java.Lang.Object, ILocationTrackingService, ILocationListener
	{
		#region private fields
		Context _context;
		#endregion

		#region .ctors
		public LocationTrackingService(Context context){
			_context = context;
		}
		#endregion

		#region ILocationTrackingService implementation
		public event EventHandler<LocationEventArgs> LocationChanged;
		public async void StartTrackingUserLocation ()
		{
			var available = await CheckUsersPermission();
			if (available) {
				var locService = LocationManager.FromContext (_context);
				var providers = locService.AllProviders;
				if (providers.Any ()) {
					string provider = null;
					if (providers.Contains (LocationManager.NetworkProvider))
						provider = LocationManager.NetworkProvider;
					if (provider == null && providers.Contains (LocationManager.GpsProvider))
						provider = LocationManager.GpsProvider;

					if (provider == null)
						provider = providers.First ();
				
					locService.RequestLocationUpdates (provider, 10000, 1000f, this);
				}
			}
		}
		public void StopTrackingUserLocation ()
		{
			var locService = LocationManager.FromContext(_context);
			locService.RemoveUpdates (this);
		}
		#endregion

		#region ILocationListener implementation

		void ILocationListener.OnLocationChanged (Location location)
		{
			LocationChanged?.Invoke(this, new LocationEventArgs(location.Latitude, location.Longitude));
		}
		void ILocationListener.OnProviderDisabled (string provider)
		{
		}
		void ILocationListener.OnProviderEnabled (string provider)
		{
		}
		void ILocationListener.OnStatusChanged (string provider, Availability status, Android.OS.Bundle extras)
		{
		}
		#endregion

		#region Permissions
		TaskCompletionSource<bool> responseSource;
		int RequestLocationId = 0;
		readonly string [] PermissionsLocation = 
		{
			Manifest.Permission.AccessFineLocation
		};

		Task<bool> CheckUsersPermission ()
		{
			return Task.Run (async () => {
				if ((int)Build.VERSION.SdkInt < 23) {
					return true;
				} else{
					const string permission = Manifest.Permission.AccessFineLocation;
					if (_context.CheckSelfPermission(permission) == (int)Permission.Granted)
					{
						return true;
					}
					//need to request permission
					var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
					if (activity.ShouldShowRequestPermissionRationale(permission))
					{
						var permissionable = activity as IPermissionable;
						responseSource = new TaskCompletionSource<bool>();
						//Explain to the user why we need to read the contacts
						activity.RunOnUiThread(()=>{
							new AlertDialog.Builder(activity)
								.SetTitle("Permission request")
								.SetMessage("Location access is required to show coffee shops nearby.")
								.SetPositiveButton("OK", (sender, e)=>{
									activity.RequestPermissions(PermissionsLocation, RequestLocationId);
								})
								.Show();
						});

						permissionable.OnPermissionGranted = ()=>{
							//Permission granted
						Toast.MakeText(activity,"Location permission is available, getting lat/long.", ToastLength.Long).Show();
							responseSource.SetResult(true);
						};
						permissionable.OnPermissionDisabled = ()=>{

						Toast.MakeText(activity,"Location permission is denied.", ToastLength.Long).Show();
							responseSource.SetResult(false);
						};

						return await responseSource.Task;
					}
					return true;
				}
			});
		}
		#endregion
	}
}

