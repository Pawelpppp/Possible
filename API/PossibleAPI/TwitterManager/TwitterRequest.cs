using OAuth;
using RestSharp;
using System;
using System.Collections.Specialized;
using System.Web;

namespace TwitterManager
{
    public class TwitterRequest : ITwitterRequest
    {
        public string UserLastTweet()
        {
            var path = "/1.1/statuses/user_timeline.json";
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["screen_name"] = "POSSIBLE";
            parameters["count"] = "5";

            var twitterLink = BuildTwitterLink(path, parameters);
            var client = new RestClient(twitterLink);

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", GetAuthorizationHeader(twitterLink));

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string Search(string searchText)
        {
            var path = "1.1/search/tweets.json";
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["q"] = searchText;
            parameters["result_type"] = "recent";
            parameters["count"] = "10";

            var twitterLink = BuildTwitterLink(path, parameters);
            var client = new RestClient(twitterLink);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", GetAuthorizationHeader(twitterLink));

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string Search(string searchText, string parameters)
        {
            var path = "1.1/search/tweets.json";

            var twitterLink = BuildTwitterLink(path, parameters);
            var client = new RestClient(twitterLink);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", GetAuthorizationHeader(twitterLink));

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        private Uri BuildTwitterLink(string path, NameValueCollection parameters)
        {
            var uri = new UriBuilder("https://api.twitter.com/");
            uri.Path = path;
            uri.Query = parameters.ToString();
            return uri.Uri;
        }

        private Uri BuildTwitterLink(string path, string parameters)
        {
            var uri = new UriBuilder("https://api.twitter.com/");
            uri.Path = path;
            uri.Query = parameters;
            return uri.Uri;
        }

        private string GetAuthorizationHeader(Uri uri)
        {
            OAuthRequest client = OAuthRequest.ForProtectedResource("GET", "qLT42Pa4Pm7FxY4v7fqtBw", "s49oOJabVbS305j5yMVWcHOp3YX9XExl8pUHEv9g", "39523825-pCBfpmVcbyopUEXtwdOmMERq7VMtPk937YKO911tj", "E2EpHYquZTRJ3NYLK9JYeGN0jGD5P8jH9bQHtdFb7JI");
            client.RequestUrl = uri.AbsoluteUri;

            var authorizationHeader = client.GetAuthorizationHeader();
            return authorizationHeader;
        }
    }
}
