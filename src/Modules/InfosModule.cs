using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Kururu.Extensions;
using Kururu.Framework;
using Kururu.Framework.Commands;
using Miki.Discord.Common.Packets;

namespace Kururu.Module
{
    [Module("Info")]
    public class InfoModule : ModuleBase 
    {
        [Command(Name = "BotInfo")]
        public async Task BotInfoCommand() 
        {
            var Data = KMath.CalculateTime(DiscordBot.Instance.UpTime, DateTime.Now);
            int UserAmount = (await Guild.GetMembersAsync()).Length;
            EmbedMaker maker = new EmbedMaker();
            maker.setTitle("Kururu's info").addField("Info", $"This bot is running in {await DiscordBot.Instance.GuildsData.CountAsync()} Servers")
            .addInlineField("Language", "c#").addInlineField("Owner", "Hedari")
            .addField("Runtime", $"This bot has been running for, {Data.Days} Day, {Data.Hours} Hour, {Data.Minutes} Minute and {Data.Seconds} Second");
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Help")]
        public async Task HelpCommand()
        {
            EmbedMaker maker = new EmbedMaker();
            var assembly = Assembly.GetExecutingAssembly();
            var Modules = assembly.GetTypes().Where(x => x.GetCustomAttribute<ModuleAttribute>() != null);
            foreach (var module in Modules)
            {
                string CommandsString = "";
                var Commands = module.GetMethods().Where(x => x.GetCustomAttribute<CommandAttribute>() != null);
                foreach (var Command in Commands) 
                {
                    if (Command.GetCustomAttribute<OwnerAttribute>() == null)
                        CommandsString += $"{Command.GetCustomAttribute<CommandAttribute>().Name}, ";   
                }
                CommandsString = CommandsString.Substring(0, CommandsString.Length-2);
                maker.addInlineField(module.Name, CommandsString);
            }
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "ServerInfo")]
        public async Task ServerInfoCommand()
        {
            try 
            {
                var Members = await Guild.GetMembersAsync();
                var Owner = await Guild.GetOwnerAsync();
                EmbedMaker maker = new EmbedMaker();
                maker.setTitle(Guild.Name).addInlineField("Owner", Owner?.Username)
                .addInlineField($"Members", $"there is {Members.Length} in the guild")
                .addInlineField("Server's Prefix", (await DiscordBot.Instance.GuildsData.GetAsync(Guild.Id.ToString())).Prefix)
                .addInlineField("Roles Amount", (await Guild.GetRolesAsync()).Count().ToString())
                .addInlineField("Channels", (await Guild.GetChannelsAsync()).Count().ToString());
                await Channel.SendMessageAsync("", false, maker);
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}