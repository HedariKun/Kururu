using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kururu.API.Waaai;
using Kururu.Framework;
using Kururu.Framework.MySql;
using Kururu.Framework.Config;
using Kururu.Framework.Commands;
using Miki.Discord.Common;

namespace Kururu.Module
{
	[Module ("General")]
	public class GeneralModules : ModuleBase
	{

		[Configuration]
		public string WaaaiKey = "";

		[Command (Name = "Say")]
		public async Task SayCommand () =>
			await Channel.SendMessageAsync (string.Join (" ", Arg));

		[Command (Name = "Ping", Alias = new string[]{"Letancy"})]
		public async Task PingCommand ()
		{
			EmbedMaker maker = new EmbedMaker ();
			maker.setTitle ("Calculating the ping, please wait...");
			Stopwatch watch = new Stopwatch ();
			watch.Start ();
			var msg = await Channel.SendMessageAsync ("", false, maker);
			watch.Stop ();
			maker.setTitle ($"Pong, the ping is {watch.ElapsedMilliseconds} ms");
			await msg.EditAsync (new EditMessageArgs () { embed = maker });
		}

		

		[Command (Name = "Avatar")]
		public async Task AvatarCommand ()
		{
			var Member = Arg [0] != null ? await GetUser () : Author;
			Member = Member == null ? Author as IDiscordGuildUser : Member;
			var permissions = await Guild.GetPermissionsAsync (Member as IDiscordGuildUser);
			EmbedMaker maker = new EmbedMaker ();
			string url = Member.GetAvatarUrl ();
			maker.setTitle ($"{Member.Username}'s profile pic").setImage (url);
			await Channel.SendMessageAsync ("", false, maker);
		}

		[Command (Name = "ShortLink")]
		public async Task ShortLinkCommand ()
		{
			if (Arg.Length < 1)
			{
				await Channel.SendMessageAsync("You need to provide a link");
			}
			WaaaiRequester LinkClient = new WaaaiRequester();
			var res = await LinkClient.GetShortLink(Arg[0], WaaaiKey);
			if (!string.IsNullOrEmpty(res.Data.Error))
			{
				await Channel.SendMessageAsync(res.Data.Error);
				return;
			}
			EmbedMaker maker = new EmbedMaker();
			maker.setTitle("Link Shorter").setDesctiption($"The shorted link is: {res.Data.Url}").setFooter("Powered by Waaai");
			await Channel.SendMessageAsync("", false, maker);
		}

		[Command(Name = "addGuild")]
		[Owner]
		public async Task AddGuild ()
		{
			if(! await Program.GuildsData.ExistAsync(Guild.Id.ToString()))
				await DiscordBot.Instance.mysqlHandler.QueryData($"INSERT INTO `guilds` (guilds.GuildID, guilds.Prefix, guilds.AddDate) VALUES ({Guild.Id}, \"~\", \"{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}\")");
		}

		[Command(Name = "addGuilds")]
		[Owner]
		public async Task AddGuilds ()
		{
			var Guilds = await DiscordBot.Instance.bot.CacheClient.HashKeysAsync(CacheUtils.GuildsCacheKey);
			foreach (var ID in Guilds) {
				if (!await Program.GuildsData.ExistAsync(ID))
					await DiscordBot.Instance.mysqlHandler.QueryData($"INSERT INTO `guilds` (guilds.GuildID, guilds.Prefix, guilds.AddDate) VALUES ({ID}, \"~\", \"{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}\")");
			}
		}


	}
}
