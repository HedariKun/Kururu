using System;
using System.Threading.Tasks;
using Kururu.Framework;
using Kururu.Framework.Commands;
using Miki.Discord.Common;

namespace Kururu
{
	public class ModuleHandler
	{
		private CommandHandle handler;
		private string Prefix;
		public ModuleHandler ()
		{
			handler = new CommandHandle ();
		}

		public async Task StartHandler ()
		{
			Prefix = await DiscordBot.Instance.cacheManger.GetAsync("Prefix");
			DiscordBot.Instance.bot.MessageCreate += HandleCommand;
		}

		public async Task HandleCommand (IDiscordMessage message)
		{

			if (message.Content.StartsWith (Prefix) == false)
				return;
			MessageContext context = new MessageContext (Prefix, message);
			await handler.ExecuteCommand (context);
		}

	}
}
