using System.Threading.Tasks;
using Kururu.API.Image;
using Kururu.Framework;
using Kururu.Framework.Commands;

namespace Kururu.Module
{
	[Module ("Image")]
	public class ImageModule : ModuleBase
	{
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
	}
}
