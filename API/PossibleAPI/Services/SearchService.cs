using Newtonsoft.Json.Linq;
using Services.Dto;
using System;
using System.Collections.Generic;
using TwitterManager;

namespace Services
{
    public class SearchService
    {
        private TwitterRequest _twitterRequest;

        public SearchService()
        {
            _twitterRequest = new TwitterRequest();
        }

        public List<TweetDto> Search(string search)
        {
            var SearchText = _twitterRequest.Search2(search);
            dynamic data = JObject.Parse(SearchText);

            return GetRequiredData(data.statuses);
        }

        private List<TweetDto> GetRequiredData(dynamic statuses)//fix dynamic
        {
            var result = new List<TweetDto>();
            foreach (var tweet in statuses)
            {
                if (!ReferenceEquals(null, tweet))
                {
                    var date = Convert.ToString(tweet["created_at"]);
                    var text = Convert.ToString(tweet["text"]);
                    var name = Convert.ToString(tweet?.user["name"]);
                    result.Add(new TweetDto(date, text, name));
                }
            }
            return result;
        }
    }
}
