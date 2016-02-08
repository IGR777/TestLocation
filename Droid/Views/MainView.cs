using Android.App;
using TestLocation.ViewModels;
using System;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.Fragging;
using Android.OS;

namespace TestLocation.Droid.Views
{
	[Activity(Label = "Weather application", Theme = "@android:style/Theme.Material.Light")]
	public class MainView : MvxFragmentActivity, IPermissionable
    {

		new MainViewModel ViewModel{
			get{
				return base.ViewModel as MainViewModel;
			}
		}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
			this.ActionBar.Hide ();
        }

		protected override void OnStart ()
		{
			base.OnStart ();
			ViewModel.StartTracking.Execute (null);
		}

		protected override void OnStop ()
		{
			base.OnStop ();
			ViewModel.StopTracking.Execute (null);
		}

		#region IPermissionable implementation
		public Action OnPermissionGranted { get; set; }
		public Action OnPermissionDisabled { get; set; }
		#endregion


		public override void OnRequestPermissionsResult (int requestCode, string[] permissions, Permission[] grantResults)
		{
			base.OnRequestPermissionsResult (requestCode, permissions, grantResults);
			switch (requestCode)
			{
			case 0:
				{
					if (grantResults[0] == (int)Permission.Granted)
					{
						//Permission granted
						if (OnPermissionGranted != null)
							OnPermissionGranted ();
					}
					else
					{
						//Permission Denied :(
						if (OnPermissionDisabled != null)
							OnPermissionDisabled ();
					}
				}
				break;
			}
		}
    }
}