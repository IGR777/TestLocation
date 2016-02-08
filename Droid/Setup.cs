using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;

namespace TestLocation.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

		protected override void InitializeLastChance ()
		{
			base.InitializeLastChance ();
			Mvx.RegisterSingleton<ILocationTrackingService> (new LocationTrackingService (ApplicationContext));
		}
    }
}