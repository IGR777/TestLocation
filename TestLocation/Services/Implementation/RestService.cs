using System;
using ModernHttpClient;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestLocation
{
	public class RestService : IRestService
	{
		#region private fields
		const string Url = "http://api.openweathermap.org/data/2.5/weather";
		const string appID = "&appid=c1c4ace529f3255d32ebdc0013df6b91";
		const string units = "&units=metric";
		#endregion

		#region IRestService implementation
		public async Task<WeatherEntity> GetWeather (double longtitude, double latitude)
		{
			var client = new HttpClient(new NativeMessageHandler());
			var parameters = String.Format ("?lat={0}&lon={1}", latitude, longtitude);
			string result = await client.GetStringAsync(Url + parameters + appID + units);
			var dto = JsonConvert.DeserializeObject<WeatherDTO>(result, new CustomJsonConverter<WeatherDTO>());
			return dto.DtoToEntity ();

		}
		#endregion		
	}
}

