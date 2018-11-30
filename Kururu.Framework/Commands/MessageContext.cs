using System.Linq;
using Miki.Discord.Common;

namespace Kururu.Framework.Commands
{
	public class MessageContext
	{
		public string Command;
		public string [] Arg;
		public IDiscordMessage Message;

		public MessageContext (string _prefix, IDiscordMessage _message)
		{
			Message = _message;
			var commandList = _message.Content.Replace (_prefix, "").Split (" ");
			Command = commandList [0].ToLower ();
			Arg = commandList.Skip (1).ToArray ();
		}


	}
}
