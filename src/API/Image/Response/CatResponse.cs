using Newtonsoft.Json;

namespace Kururu.API.Image
{
	public class CatResponse
	{
		[JsonProperty ("file")]
		public string Url;
	}
}
