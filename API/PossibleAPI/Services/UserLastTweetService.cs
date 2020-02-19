using Newtonsoft.Json.Linq;
using TwitterManager;

namespace Services
{
    public class UserLastTweetService
    {
        private TwitterRequest _twitterRequest;

        public UserLastTweetService()
        {
            _twitterRequest = new TwitterRequest();
        }

        public void UserLastTweet(string name)
        {
            var SearchText = _twitterRequest.UserLastTweet();
            //dynamic data = JObject.Parse(SearchText);

        }
    }
}
