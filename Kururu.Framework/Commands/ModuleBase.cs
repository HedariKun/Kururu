using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kururu.Framework;
using Miki.Discord.Common;

namespace Kururu.Framework.Commands
{
	public class ModuleBase
	{
		public IDiscordMessage Message;
		public IDiscordTextChannel Channel;
		public IDiscordUser Author;
		public IDiscordGuild Guild;
		public string [] Arg;

		public async Task<IDiscordGuildUser> GetUser (int argPos = 0)
		{
			if (Arg.Count () <= 0)
				return null;

			string Data = "";
			for (int i = argPos; i < Arg.Length; i++)
				Data += Data == "" ? Arg [i] : $" {Arg[i]}";

			List<IDiscordGuildUser> Users = new List<IDiscordGuildUser> ();
			var Members = await Guild.GetMembersAsync ();
			if (Data.StartsWith ("<@"))
			{
				Data = Data.Replace ("<@!", "");
				Data = Data.Replace ("<@", "");
				Data = Data.Replace (">", "");
			}
			Data = Data.ToLower ();
			bool userFound = false;
			foreach (var Member in Members)
			{

				if (Member.Username.Length >= Data.Length)
				{
					userFound = Member.Username.ToLower ().StartsWith (Data);
				}
				if (Member.Nickname != null && Member.Nickname.Length >= Data.Length)
				{
					userFound = Member.Nickname.ToLower ().StartsWith (Data);
				}
				try
				{
					userFound = Member.Id == Convert.ToUInt64 (Data);
				}
				catch (Exception)
				{

				}
				if (userFound)
				{
					return Member;
				}
			}
			return null;
		}

	}
}
