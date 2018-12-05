using System;
using System.Linq;
using System.Threading.Tasks;
using Kururu.Extensions;
using Kururu.Framework;
using Kururu.Framework.Commands;

namespace Kururu.Module
{
    [Module("Info")]
    public class InfoModule : ModuleBase 
    {
        [Command("BotInfo")]
        public async Task GetBotInfoCommand() 
        {
            var Data = KMath.CalculateTime(DiscordBot.Instance.UpTime, DateTime.Now);
            int UserAmount = (await Guild.GetMembersAsync()).Length;
            EmbedMaker maker = new EmbedMaker();
            maker.setTitle("Kururu's info").addField("Info", $"This bot is running in {await DiscordBot.Instance.GuildsData.CountAsync()} Servers")
            .addInlineField("Language", "c#").addInlineField("Owner", "Hedari")
            .addField("Runtime", $"This bot has been running for, {Data.Days} Day, {Data.Hours} Hour, {Data.Minutes} Minute and {Data.Seconds} Second");
            await Channel.SendMessageAsync("", false, maker);
        }
    }
}