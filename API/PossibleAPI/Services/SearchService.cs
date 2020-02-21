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

        public List<TweetDto> Search(string search, int page)
        {
            var searchText = _twitterRequest.Search(search);
            if (!string.IsNullOrEmpty(searchText))
            {
                
                dynamic data = JObject.Parse(searchText);

                return GetRequiredData(data.statuses);
            }
            return null;
            
        }

        private List<TweetDto> GetRequiredData(dynamic statuses)//fix dynamic
        {
            var result = new List<TweetDto>();
            foreach (var tweet in statuses)
            {
                if (!ReferenceEquals(null, tweet))
                {
                    string date = Convert.ToString(tweet["created_at"]);
                    string text = Convert.ToString(tweet["text"]);
                    string name = Convert.ToString(tweet?.user["name"]);
                    result.Add(new TweetDto(date, text, name));
                }
            }
            return result;
        }
    }
}
