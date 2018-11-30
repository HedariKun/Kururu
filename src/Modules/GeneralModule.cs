using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kururu.Framework;
using Kururu.Framework.Commands;
using Miki.Discord.Common;

namespace Kururu.Module
{
	[Module ("General")]
	public class GeneralModules : ModuleBase
	{
		[Command ("Say")]
		public async Task SayCommand () => await Channel.SendMessageAsync (string.Join (" ", Arg));

		[Command ("Ping")]
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

		[Command ("Choose")]
		public async Task ChooseCommand ()
		{
			if (Arg.Length < 2)
			{
				await Channel.SendMessageAsync ("you need to provide 2 or more choices");
				return;
			}

			Random random = new Random ();
			string Choice = Arg [random.Next (Arg.Length)];
			await Channel.SendMessageAsync ($"I'll choose: **{Choice}**");
		}

		[Command ("Avatar")]
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

	}
}
