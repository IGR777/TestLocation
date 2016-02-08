using System;
using MvvmCross.Platform.Converters;

namespace TestLocation.Droid
{
	public class TemperatureValueConverter:MvxValueConverter<int,string>
	{
		protected override string Convert (int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return String.Format ("{0} \u2103", value);
		}
	}
}

