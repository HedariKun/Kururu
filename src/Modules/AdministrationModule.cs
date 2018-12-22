using System.Linq;
using System.Threading.Tasks;
using Miki.Discord.Common;
using Kururu.Common;
using Kururu.Framework;
using Kururu.Framework.MySql;
using Kururu.Framework.Commands;

namespace Kururu.Module
{
	[Module ("Administration")]
	public class AdministrationModule : ModuleBase
	{
		[Command (Name = "kick", Permission = GuildPermission.KickMembers)]
		public async Task KickMember ()
		{
			var Member = await GetUser ();
			if (Member != null)
			{
				EmbedMaker Maker = new EmbedMaker ();
				Maker.setTitle ("Kick Command").setDesctiption ($"{Author.Username} has Kicked {Member.Username}");
				await Member.KickAsync ("Kick Command");
				await Channel.SendMessageAsync ("", false, Maker);
			}
		}

		[Command (Name = "ban", Permission = GuildPermission.BanMembers)]
		public async Task BanMember ()
		{
			var Member = await GetUser ();
			if (Member != null)
			{
				EmbedMaker Maker = new EmbedMaker ();
				Maker.setTitle ("Ban Command").setDesctiption ($"{Author.Username} has banned {Member.Username}");
				await Guild.AddBanAsync (Member);
				await Channel.SendMessageAsync ("", false, Maker);
			}
		}

		[Command(Name = "SetPrefix", Permission = GuildPermission.Administrator)]
		public async Task SetPrefixCommand()
		{
			if(Arg.Length <= 0)
			{
				await Channel.SendMessageAsync("You need To Provide a Prefix");
				return;
			}
			await DiscordBot.Instance.mysqlHandler.QueryData($"Update guilds SET guilds.Prefix=\"{Arg[0]}\" WHERE guilds.GuildID={Guild.Id}");
			var NewData = await DiscordBot.Instance.mysqlHandler.QueryData<GuildData>($"SELECT * FROM guilds WHERE guilds.GuildID={Guild.Id}");
			await Program.GuildsData.UpdateAsync(Guild.Id.ToString(), NewData[0]);
			await Channel.SendMessageAsync($"The Server's Prefix Updated To: **{Arg[0]}**");
		}

	}
}
