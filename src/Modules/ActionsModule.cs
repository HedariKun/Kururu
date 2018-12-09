using System.Threading.Tasks;
using Kururu.Framework;
using Kururu.Framework.Config;
using Kururu.Framework.Commands;
using Nekos.Sharp;

namespace Kururu.Module
{
    [Module("Actions")]
    public class ActionsModule : ModuleBase
    {
        [Command(Name = "Hug")]
        public async Task HugCommed ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Hug);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} hugs {Member.Username}" : $"{Author.Username} is alone ;-;").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Cuddle")]
        public async Task CuddleCommed ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Cuddle);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} cuddles {Member.Username}" : $"{Author.Username} is alone ;-;").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Pat")]
        public async Task PatCommand ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Pat);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} pats {Member.Username}" : $"{Author.Username} is alone ;-;").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Kiss")]
        public async Task KissCommand ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Kiss);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} kisses {Member.Username}" : $"{Author.Username}, are you a narcissistic?").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Poke")]
        public async Task PokeCommand ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Poke);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} pokes {Member.Username}" : $"{Author.Username} pokes himself, weirdo").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Slap")]
        public async Task SlapCommed ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Slap);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} slaps {Member.Username}" : $"{Author.Username} slapped himself, retarded").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Tickle")]
        public async Task TickleCommand ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Tickle);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} tickles {Member.Username}" : $"{Author.Username} tickles himself, okay?").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Feed")]
        public async Task FeedCommand ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Feed);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} feeds {Member.Username}" : $"{Author.Username} feeds himself").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Baka")]
        public async Task BakaCommand ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Baka);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} call {Member.Username} a baka" : $"{Author.Username} is shouting Baka").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }

        [Command(Name = "Meow")]
        public async Task MeowCommand ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Meow);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} meows at {Member.Username}" : $"{Author.Username} is meowing").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }
        
        [Command(Name = "Smug")]
        public async Task SmugCommand ()
        {
            var Member = await GetUser();
            EmbedMaker maker = new EmbedMaker();
            var image = await NekoClient.GetImageAsync(SFWTypes.Smug);
            maker.setTitle(Member != null && Member.Id != Author.Id ? $"{Author.Username} smugs at {Member.Username}" : $"{Author.Username} smugged").setImage(image.ImageURl);
            await Channel.SendMessageAsync("", false, maker);
        }


    }
}