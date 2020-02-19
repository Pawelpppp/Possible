using Newtonsoft.Json.Linq;
using OAuth;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace TwitterManager
{
    public class TwitterRequest
    {

        public string UserLastTweet()
        {
            var client = new RestClient("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=POSSIBLE&count=10");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "OAuth " +
                "oauth_consumer_key=\"qLT42Pa4Pm7FxY4v7fqtBw\"," +
                "oauth_token=\"39523825-pCBfpmVcbyopUEXtwdOmMERq7VMtPk937YKO911tj\"," +
                "oauth_signature_method=\"HMAC-SHA1\"," +
                "oauth_timestamp=\"1582144750\"," +
                "oauth_nonce=\"VDR9cKhgvbQ\"," +
                "oauth_version=\"1.0\"," +
                "oauth_signature=\"BH0mcZMowZrBwMIAviw1jRWuYOc%3D\"");
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string Search(string searchText)
        {
            var path = "1.1/search/tweets.json";
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["q"] = searchText;
            parameters["result_type"] = "mixed";
            parameters["count"] = "10";

            var twitterLink = BuildTwitterLink( path, parameters);
            var client = new RestClient(twitterLink);
            //var client = new RestClient("https://api.twitter.com/1.1/search/tweets.json?q=Polska&result_type=mixed&count=10");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "OAuth oauth_consumer_key=\"qLT42Pa4Pm7FxY4v7fqtBw\",oauth_token=\"39523825-pCBfpmVcbyopUEXtwdOmMERq7VMtPk937YKO911tj\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"1582149312\",oauth_nonce=\"US2bAoWJzH3\",oauth_version=\"1.0\",oauth_signature=\"xRxAW8uD58PeQSqiAg2DyoIrGfE%3D\"");
            //string OAuth = GetAuthorizationHeader();
            //request.AddHeader("OAuth", OAuth);


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

        private string GetAuthorizationHeader()
        {
            OAuthRequest client = OAuthRequest.ForProtectedResource("GET", "qLT42Pa4Pm7FxY4v7fqtBw", "s49oOJabVbS305j5yMVWcHOp3YX9XExl8pUHEv9g", "39523825-pCBfpmVcbyopUEXtwdOmMERq7VMtPk937YKO911tj", "E2EpHYquZTRJ3NYLK9JYeGN0jGD5P8jH9bQHtdFb7JI");
            client.RequestUrl = "https://api.twitter.com/1.1/account/verify_credentials.json?include_email=true";

            var authorizationHeader = client.GetAuthorizationQuery();
            return authorizationHeader;
        }
    }
}
