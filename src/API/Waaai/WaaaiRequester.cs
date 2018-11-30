using System.Threading.Tasks;
using Kururu.Framework;

namespace Kururu.API.Waaai
{
    public class WaaaiRequester
    {
        private string _baseUrl = "https://api.waa.ai/shorten?";
        public async Task<WaaaiResponse> GetShortLink (string link,  string key = "") => await APIRequestor.GetRequest<WaaaiResponse>($"{_baseUrl}url={link}&key={key}");
    }
}