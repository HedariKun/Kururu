using System;
using System.Threading.Tasks;
using Kururu.Framework;

namespace Kururu
{
	class Program
	{
		public static async Task Main ()
		{
			BotHandler handler = new BotHandler ();
			DiscordBot client = new DiscordBot (".config");
			await client.configManger.LoadData(client.cacheManger);
			await client.Setup(await client.cacheManger.GetAsync("Token"));
			await handler.StartHandler ();
			await client.StartAsync ();
			await Task.Delay (-1);
		}  
	}
}
