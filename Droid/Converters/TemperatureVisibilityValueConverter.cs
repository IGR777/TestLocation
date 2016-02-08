using System;
using Android.Views;
using MvvmCross.Platform.Converters;

namespace TestLocation.Droid
{
	public class TemperatureVisibilityValueConverter:MvxValueConverter<int?, object>
	{
		protected override object Convert (int? value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !value.HasValue ? ViewStates.Gone : ViewStates.Visible;
		}
	}
}

