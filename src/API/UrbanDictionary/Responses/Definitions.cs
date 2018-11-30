using Newtonsoft.Json;

namespace Kururu.API.UrbanDictionary
{
    public class UrbanResponse
    {
        [JsonProperty("Tags")]
        public string[] Tags;
        [JsonProperty("result_type")]
        public string ResultType;
        [JsonProperty("list")]
        public UrbanDefinition[] DefinitionsList;
    }

    public class UrbanDefinition
    {
        [JsonProperty("defid")]
        public int ID;
        [JsonProperty("word")]
        public string Word;
        [JsonProperty("author")]
        public string Author;
        [JsonProperty("permalink")]
        public string Link;
        [JsonProperty("definition")]
        public string Definition;
        [JsonProperty("example")]
        public string Example;
        [JsonProperty("thumbs_up")]
        public int ThumbsUp;
        [JsonProperty("thumbs_down")]
        public int ThmbsDown;
        [JsonProperty("current_vote")]
        public string CurrentVote;
        [JsonProperty("written_on")]
        public string PostDate = "";
    }
}