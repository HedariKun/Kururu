using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Kururu.Extensions;
using Kururu.Framework;
using Kururu.Framework.Commands;
using Miki.Discord.Common.Packets;
using System.Diagnostics;

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
            var commandCallValue = await DiscordBot.Instance.cacheManger.GetAsync("CommandCall");
            var messageCallValue = await DiscordBot.Instance.cacheManger.GetAsync("MessageCall");
            var memoryUsage = Process.GetCurrentProcess().PrivateMemorySize64 / 1000 / 1000;
            EmbedMaker maker = new EmbedMaker();
            maker.setTitle("Kururu's info")
            .addField("Info", $"This bot is running in {await Program.GuildsData.CountAsync()} Servers")
            .addInlineField("Language", "c#").addInlineField("Owner", "Hedari")
            .addInlineField("Commands", commandCallValue)
            .addInlineField("Messages", messageCallValue)
            .addInlineField("Memory Usage", $"{memoryUsage} MB")
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
                maker.addField(module.Name.Replace("Module", ""), CommandsString);
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
                .addInlineField("Server's Prefix", (await Program.GuildsData.GetAsync(Guild.Id.ToString())).Prefix)
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