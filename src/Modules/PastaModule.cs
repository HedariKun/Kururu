using System;
using System.Threading.Tasks;
using Kururu.Common;
using Kururu.Framework;
using Kururu.Framework.Commands;

namespace Kururu.Module
{  
    [Module("PastaModule")]
    public class PastaModule : ModuleBase
    {
        [Command(Name="Pasta")]
        public async Task GetPasta ()
        {
            if (Arg.Length < 1)
            {
                await Channel.SendMessageAsync("Please provide a Pasta's Name");
                return;
            }
            var pastaDatas = await DiscordBot.Instance.mysqlHandler.QueryData<PastaData>($"SELECT * FROM pasta WHERE pasta.Key=\"{Arg[0]}\" COLLATE utf8_bin");
            if (pastaDatas.Count < 1) 
            {
                await Channel.SendMessageAsync("Pasta wasn't found");
                return;
            } 
            await Channel.SendMessageAsync(pastaDatas[0].Value);
        }

        [Command(Name="savePasta")]
        public async Task CreatePasta()
        {
            if (Arg.Length < 2)
            {
                await Channel.SendMessageAsync("Please Provide a name and value to the pasta");
                return;
            }
            var pastaDatas = await DiscordBot.Instance.mysqlHandler.QueryData<PastaData>($"SELECT * FROM pasta WHERE pasta.Key=\"{Arg[0]}\" COLLATE utf8_bin");
            if (pastaDatas.Count > 0)
            {
                await Channel.SendMessageAsync("The pasta already exist.");
                return;
            }
            string context = "";
            for (int i = 1; i < Arg.Length; i++)
            {
                context += $"{Arg[i]} ";
            }
            await DiscordBot.Instance.mysqlHandler.QueryData(
                $"INSERT INTO pasta VALUES (\"{Arg[0]}\", \"{context}\", {Author.Id}, {Guild.Id}, \"{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}\")"
            );
            await Channel.SendMessageAsync($"Pasta `{Arg[0]}` was Saved successfully");
        }

    }
}