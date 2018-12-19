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
			var commandList = _message.Content.Substring(_prefix.Length, _message.Content.Length-1).Split (" ");
			Command = commandList [0].ToLower ();
			Arg = commandList.Skip (1).ToArray ();
		}


	}
}
