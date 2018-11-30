using Newtonsoft.Json;

namespace Kururu.API.Yandere
{
    public class YandereImage
    {
        [JsonProperty("id")]
        public int ID;

        [JsonProperty("created_at")]
        public long CreatedAt;

        [JsonProperty("author")]
        public string Author;

        [JsonProperty("file_size")]
        public long FileSize;

        [JsonProperty("file_ext")]
        public string FileExtension;

        [JsonProperty("file_url")]
        public string ImageUrl;

        [JsonProperty("rating")]
        public char Rating;

        [JsonProperty("width")]
        public int Width;

        [JsonProperty("height")]
        public int Height;
    }
}