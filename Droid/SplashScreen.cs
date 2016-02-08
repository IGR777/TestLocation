using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace TestLocation.Droid
{
    [Activity(
		Label = "TestLocation.Droid"
		, MainLauncher = true
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}