using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kururu.Framework
{
	public class APIRequestor
	{
		public async static Task<T> GetRequest<T> (string url) where T : new ()
		{
			using (HttpClient client = new HttpClient ()) 
			{
			string data = await client.GetStringAsync (url);
			return string.IsNullOrEmpty (data) ? new T () : JsonConvert.DeserializeObject<T> (data);
			}
		}
	}
}
