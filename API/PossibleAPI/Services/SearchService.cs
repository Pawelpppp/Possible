using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
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

        public void Search(string search)
        {
            var SearchText = _twitterRequest.Search(search);
            dynamic data = JObject.Parse(SearchText);

        }
    }
}
