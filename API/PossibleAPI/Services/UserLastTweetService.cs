using Newtonsoft.Json.Linq;
using Services.Dto;
using System;
using System.Collections.Generic;
using TwitterManager;

namespace Services
{
    public class UserLastTweetService : IUserLastTweetService
    {
        private ITwitterRequest _twitterRequest;

        public UserLastTweetService(ITwitterRequest twitterRequest)
        {
            _twitterRequest = twitterRequest;
        }

        public List<TweetDto> UserLastTweet(string name)
        {
            var SearchText = _twitterRequest.UserLastTweet();
            if (!string.IsNullOrEmpty(SearchText))
            {
                dynamic data = JArray.Parse(SearchText);
                return GetRequiredData(data);
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
