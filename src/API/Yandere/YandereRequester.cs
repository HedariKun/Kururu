using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Kururu.Framework;

namespace Kururu.API.Yandere
{
    public enum YandereRating
    {
        Safe,
        Questionable,
        Explicit,
        all
    }
    public class YandereTag
    {
        public List<string> Tags = new List<string>();

        public YandereRating Rating = YandereRating.all;
        public int Limit = 100;
        public int Page = 1;

        public string GenerateTags()
        {
            string TagString = Tags.Aggregate((i, b) => $"{i}+{b}");
            if (Rating != YandereRating.all)
            {
                switch (Rating)
                {
                    case YandereRating.Safe:
                        TagString = $"rating:s+{TagString}";
                        break;
                    case YandereRating.Questionable:
                        TagString = $"rating:q+{TagString}";
                        break;
                    case YandereRating.Explicit:
                        TagString = $"rating:e+{TagString}";
                        break;
                }
            }

            return $"tags={TagString}&limit={Limit}&page={Page}";
        }
    }
    public class YandereRequester
    {
        private string BaseUrl = "https://yande.re/post.json?";
        public async Task<List<YandereImage>> GetImagesAsync (YandereTag Tag)
        {
            string URL = $"{BaseUrl}{Tag.GenerateTags()}";
            return await APIRequestor.GetRequest<List<YandereImage>>(URL);
        }
    }
}