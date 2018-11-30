using System.Threading.Tasks;
using Kururu.Framework;

namespace Kururu
{
	class Program
	{
		public static async Task Main ()
		{
			ModuleHandler handler = new ModuleHandler ();
			DiscordBot client = new DiscordBot ("config");
			await client.configManger.LoadData(client.cacheManger);
			client.Setup(await client.cacheManger.GetAsync("Token"));
			handler.StartHandler ();
			await client.StartAsync ();
			await Task.Delay (-1);
		}  
	}
}
