using System.Threading.Tasks;
using Kururu.Framework;

namespace Kururu.API.UrbanDictionary
{
    public class UrbanRequester
    {
        public async Task<UrbanResponse> GetDefinition (string word) => await APIRequestor.GetRequest<UrbanResponse>($"http://api.urbandictionary.com/v0/define?term={word}");
    }
}