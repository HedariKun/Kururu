using Newtonsoft.Json;

namespace Kururu.API.Image
{
	public class FoxResponse
	{
		[JsonProperty ("image")]
		public string Url;
	}
}
