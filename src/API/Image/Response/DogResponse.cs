using Newtonsoft.Json;

namespace Kururu.API.Image
{
	public class DogResponse
	{
		[JsonProperty ("message")]
		public string Url;
	}
}
