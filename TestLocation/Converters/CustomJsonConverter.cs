using System;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace TestLocation
{
	public class CustomJsonConverter<T>: JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(T));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			object instance = Activator.CreateInstance(objectType);
			var props = objectType.GetTypeInfo().DeclaredProperties;

			JObject jo = JObject.Load(reader);
			var jsonProperties = jo.Properties ();
			foreach (var propItem in props) {
				var jsonAttr = propItem.GetCustomAttribute<JsonPropertyAttribute> ();
				if (jsonAttr == null) {
					continue;
				}
				var path = jsonAttr.PropertyName.Split ('/');
				JToken jToken = null;;
				foreach (var pathItem in path) {
					if (jToken == null) {
						var prop = jsonProperties.FirstOrDefault (jpItem => jpItem.Name == pathItem);
						//break if property not found
						if (prop == null)
							break;
						jToken = prop.Value;
					}
					else {
						// break if property hasn't children
						if (!jToken.HasValues)
							break;
						if(jToken is JArray)
							jToken = jToken.First.SelectToken (pathItem);
						else
							jToken = jToken.SelectToken (pathItem);

					}
				}
				if (jToken == null)
					continue;
				propItem.SetValue(instance,jToken.ToObject(propItem.PropertyType, serializer));
			}
			return instance;
		}

		public override bool CanWrite { get { return false; } }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}

