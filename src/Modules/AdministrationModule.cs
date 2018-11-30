using System.Threading.Tasks;
using Miki.Discord.Common;
using Kururu.Framework;
using Kururu.Framework.Commands;

namespace Kururu.Module
{
	[Module ("Administration")]
	public class AdministrationModule : ModuleBase
	{
		[Command ("kick")]
		[Permission (GuildPermission.KickMembers)]
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

		[Command ("ban")]
		[Permission (GuildPermission.BanMembers)]
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

	}
}
