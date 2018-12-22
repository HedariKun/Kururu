using System;
using System.Threading.Tasks;
using Kururu.Common;
using Kururu.Framework;
using Kururu.Framework.MySql;
using Kururu.Framework.Cache;
using Kururu.Framework.Commands;
using Miki.Discord.Common;

namespace Kururu
{
	public class BotHandler
	{
		private CommandHandle _handler;
		private string _prefix;

		public BotHandler ()
		{

			_handler = new CommandHandle ();
		}

		public async Task StartHandler ()
		{
			var Guilds = await DiscordBot.Instance.mysqlHandler.QueryData<GuildData>("SELECT * FROM guilds");
			foreach(var Guild in Guilds) {
				await Program.GuildsData.AddAsync(Guild.GuildID.ToString(), Guild);
			}
			_prefix = await DiscordBot.Instance.cacheManger.GetAsync("Prefix");
			DiscordBot.Instance.bot.MessageCreate += HandleCommand;
			DiscordBot.Instance.bot.GuildJoin += JoinGuild;
		}

		public async Task JoinGuild (IDiscordGuild guild)
		{
			if (!await Program.GuildsData.ExistAsync(guild.Id.ToString()))
			{
				await DiscordBot.Instance.mysqlHandler.QueryData(
					$"INSERT INTO `guilds` (guilds.GuildID, guilds.Prefix, guilds.AddDate) VALUES ({guild.Id}, \"{_prefix}\", \"{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}\")"
				);
			}
		}

		public async Task HandleCommand (IDiscordMessage message)
		{
			var messageCallValue = int.Parse(await DiscordBot.Instance.cacheManger.GetAsync("MessageCall")) + 1;
			await DiscordBot.Instance.cacheManger.UpdateAsync("MessageCall", messageCallValue.ToString());
			if (message.Author.IsBot)
				return;
			var guild = await ((IDiscordGuildChannel) await message.GetChannelAsync()).GetGuildAsync();
			var guildData = await Program.GuildsData.GetAsync(guild.Id.ToString());
			var Prefix = guildData?.Prefix != null ? guildData.Prefix : _prefix;
			var botUser = await DiscordBot.Instance.bot.GetCurrentUserAsync();
			var MentionPrefix = "";
			if (message.Content.StartsWith($"<@!{botUser.Id}>"))
				MentionPrefix = $"<@!{botUser.Id}> ";
			
			if (message.Content == $"<@!{botUser.Id}>" || message.Content == (await DiscordBot.Instance.bot.GetCurrentUserAsync()).Mention)
			{
				var Channel = await message.GetChannelAsync();
				await Channel.SendMessageAsync($"Haiii <3, The Prefix for this Bot is {Prefix} \n to See all Commands use {Prefix}help Command");
                return;
			}
			if (message.Content.StartsWith(Prefix))
			{
				MessageContext context = new MessageContext (Prefix, message);
				var commandCallValue = int.Parse(await DiscordBot.Instance.cacheManger.GetAsync("CommandCall")) + 1;
				await DiscordBot.Instance.cacheManger.UpdateAsync("CommandCall", commandCallValue.ToString());
				await _handler.ExecuteCommand (context);
			} 
			else if (!string.IsNullOrEmpty(MentionPrefix))
			{
				MessageContext context = new MessageContext (MentionPrefix, message);
				await _handler.ExecuteCommand (context);
			}
		}

	}
}
