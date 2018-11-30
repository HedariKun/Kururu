using System;
using System.Linq;
using System.Threading.Tasks;
using Kururu.API.Image;
using Kururu.API.Yandere;
using Kururu.Framework;
using Kururu.Framework.Config;
using Kururu.Framework.Commands;
using GiphySharp;
using GiphySharp.Objects;

namespace Kururu.Module
{
	[Module ("Image")]
	public class ImageModule : ModuleBase
	{

		[Configuration]
		public string GiphyKey;

		[Command ("Cat")]
		public async Task CatCommand ()
		{
			var res = await APIRequestor.GetRequest<CatResponse> ("http://aws.random.cat/meow");
			EmbedMaker maker = new EmbedMaker ().setTitle (":cat:, Nyan!").setImage (res.Url);
			await Channel.SendMessageAsync ("", false, maker);
		}

		[Command ("Dog")]
		public async Task DogCommand ()
		{
			var res = await APIRequestor.GetRequest<DogResponse> ("https://dog.ceo/api/breeds/image/random");
			EmbedMaker maker = new EmbedMaker ().setTitle (":dog:, Bark!").setImage (res.Url);
			await Channel.SendMessageAsync ("", false, maker);
		}

		[Command ("Fox")]
		public async Task FoxCommand ()
		{
			var res = await APIRequestor.GetRequest<FoxResponse> ("https://randomfox.ca/floof");
			EmbedMaker maker = new EmbedMaker ().setTitle (":fox:, Floof!").setImage (res.Url);
			await Channel.SendMessageAsync ("", false, maker);
		}

		[Command("Yandere")]
		public async Task YandereCommand ()
		{
			if (Arg.Length < 1)
			{
				await Channel.SendMessageAsync("please provide some tags");
				return;
			}
			YandereTag yandereTag = new YandereTag();
			foreach (var tag in Arg)
			{
				yandereTag.Tags.Add(tag);
			}
			yandereTag.Rating = YandereRating.Safe;
			try 
			{
				if (Channel.IsNsfw)
					yandereTag.Rating = YandereRating.Explicit;
			}
			catch (Exception)
			{
				
			}
			var res = await new YandereRequester().GetImagesAsync(yandereTag);
			if (res.Count < 1)
			{
				await Channel.SendMessageAsync("couldn't find any image with that tag");
				return;
			}
			EmbedMaker maker = new EmbedMaker().setTitle(yandereTag.Tags.Aggregate((a, b) => $"{b}, {a}")).setImage(res[(new Random().Next(res.Count))].ImageUrl);
			await Channel.SendMessageAsync("", false, maker);
		}

		[Command("Gif")]
		public async Task GifCommand ()
		{
			string Tag = Arg.Length < 1 ? Tag = "Anime" : Tag = Arg[0];
			var Client = new GiphyClient(GiphyKey);
			var Result = await Client.Gifs.SearchAsync(Tag);
			if (Result.Gifs.Count < 1)
			{
				await Channel.SendMessageAsync("couldn't find any result");
				return;
			}
			EmbedMaker maker = new EmbedMaker();
			maker.setTitle(Tag).setImage(Result.Gifs.ElementAt(new Random().Next(Result.Gifs.Count)).Images.Original.Url);
			await Channel.SendMessageAsync("", false, maker);
		}

	}
}
