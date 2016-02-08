using System;
using System.Threading.Tasks;

namespace TestLocation
{
	public interface IRestService
	{
		Task<WeatherEntity> GetWeather(double longtitude, double latitude);
	}
}

