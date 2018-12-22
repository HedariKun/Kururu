using System;
using System.Threading.Tasks;
using Kururu.Common;
using Kururu.Framework;
using Kururu.Framework.Cache;

namespace Kururu
{
	class Program
	{
		public static CacheManger<GuildData> GuildsData = new CacheManger<GuildData>();

		public static async Task Main ()
		{
			try 
			{
				BotHandler handler = new BotHandler ();
				DiscordBot client = new DiscordBot (".config");
				await client.configManger.LoadData(client.cacheManger);
				await client.Setup(await client.cacheManger.GetAsync("Token"));
				await client.cacheManger.AddAsync("CommandCall", "0");
				await client.cacheManger.AddAsync("MessageCall", "0");
				await handler.StartHandler ();
				await client.StartAsync ();
				await Task.Delay (-1);
			} 
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Environment.Exit(1);
			}
		}  
	}
}
