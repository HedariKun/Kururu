using System;
using System.Threading.Tasks;
using Kururu.Framework.Cache;
using Kururu.Framework.MySql;
using Kururu.Framework.Config;
using Miki.Cache;
using Miki.Cache.InMemory;
using Miki.Discord;
using Miki.Discord.Caching.Stages;
using Miki.Discord.Common;
using Miki.Discord.Gateway.Centralized;
using Miki.Discord.Rest;
using Miki.Logging;
using Miki.Net.WebSockets;
using Miki.Serialization.Protobuf;

namespace Kururu.Framework
{
	public class DiscordBot
	{

		public static DiscordBot Instance { get; private set; }
		public DiscordClient bot { get; private set; }
		public CacheManger<string> cacheManger;
		private IGateway _gateway;
		public ConfigManger configManger;
		public MysqlHandler mysqlHandler;
		public CacheManger<GuildData> GuildsData;
		public DiscordBot (string ConfigPath)
		{
			cacheManger = new CacheManger<string> ();
			GuildsData = new CacheManger<GuildData>();
			configManger = new ConfigManger(ConfigPath);
		}

		public async Task Setup (string Token)
		{
			if (await cacheManger.ExistAsync("DBHost"))
			{
				mysqlHandler = new MysqlHandler(new MysqlConfig() {
					DBHost = await cacheManger.GetAsync("DBHost"),
					DBUser = await cacheManger.GetAsync("DBUser"),
					DBPassword = await cacheManger.GetAsync("DBPassword"),
					DBName = await cacheManger.GetAsync("DBName")
				});
			}
			

			Log.OnLog += (string msg, LogLevel level) =>
			{
				if (level >= LogLevel.Information)
				{
					Console.WriteLine (msg);
				}
			};

			IExtendedCacheClient cache = new InMemoryCacheClient (
				new ProtobufSerializer ()
			);

			DiscordApiClient api = new DiscordApiClient (Token, cache);

			_gateway = new CentralizedGatewayShard (new GatewayConfiguration
			{
				ShardCount = 1,
					ShardId = 0,
					Token = Token,
					WebSocketClient = new BasicWebSocketClient ()
			});

			DiscordClient bot = new DiscordClient (new DiscordClientConfigurations
			{
				ApiClient = api,
					Gateway = _gateway,
					CacheClient = cache
			});
			new BasicCacheStage ().Initialize (_gateway, cache);
			this.bot = bot;
			DiscordBot.Instance = this;
		}

		public async Task StartAsync ()
		{
			try 
			{
				await _gateway.StartAsync ();
				var Guilds = await DiscordBot.Instance.mysqlHandler.QueryData<GuildData>("SELECT * FROM guilds");
				foreach(var Guild in Guilds) {
					await GuildsData.AddAsync(Guild.GuildID.ToString(), Guild);
				}
			} catch (Exception)
			{
				Console.WriteLine("bots discconected, attempting to reconnect...");
				await _gateway.StartAsync();
			}
		}

	}
}
