using Newtonsoft.Json;

namespace Kururu.API.Waaai
{
    public class WaaaiObject
    {
        [JsonProperty("url")]
        public string Url;
        [JsonProperty("short_code")]
        public string ShortCode;
        [JsonProperty("extension")]
        public string Extension;
        [JsonProperty("delete_link")]
        public string DeleteLink;
        [JsonProperty("delete_hash")]
        public string DeleteHash;
        [JsonProperty("long_url")]
        public string LongUrl;
        [JsonProperty("error")]
        public string Error = "";
    }

    public class WaaaiResponse
    {
        [JsonProperty("data")]
        public WaaaiObject Data;
        [JsonProperty("success")]
        public bool Success;
        [JsonProperty("status")]
        public int Status;
    }
}