using System;
using System.Threading.Tasks;
using Kururu.API.UrbanDictionary;
using Kururu.Framework;
using Kururu.Framework.Config;
using Kururu.Framework.Commands;

namespace Kururu.Module
{
	[Module ("fun")]
	public class FunModule : ModuleBase
	{
		[Command ("ship")]
		public async Task ShipCommand ()
		{
			var Member = await GetUser ();
			if (Member == null)
			{
				await Channel.SendMessageAsync ("you need to select user to ship with");
				return;
			}
			long Time = DateTime.Today.Ticks;
			ulong ShipAmount = Convert.ToUInt64 (Time) + Member.Id + Author.Id;
			ShipAmount %= 101;
			// Lexie and Taki love - lmao
			if (Author.Id == 456449447031996419 && Member.Id == 366036677413699585)
				ShipAmount = 100;
			if (Author.Id == 366036677413699585 && Member.Id == 456449447031996419)
				ShipAmount = 100;
			await Channel.SendMessageAsync ($"The love rate between {Author.Username} and {Member.Username} is {ShipAmount}%");
		}

		[Command("Urban")]
		public async Task UrbanCommand ()
		{
			if(Arg.Length < 1) 
			{
				await Channel.SendMessageAsync("you need to provide definition");
				return;
			}
			var res = await new UrbanRequester().GetDefinition(Arg[0]);
			EmbedMaker maker = new EmbedMaker();
			maker.setTitle($"definition of {Arg[0]}").addField("Definition", res.DefinitionsList[0].Definition).addField("Example", res.DefinitionsList[0].Example).setFooter($"by {res.DefinitionsList[0].Author}, {res.DefinitionsList[0]?.PostDate}");
			await Channel.SendMessageAsync("", false, maker);
		}

	}
}
