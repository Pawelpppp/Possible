using Services.Dto;
using System.Collections.Generic;

namespace Services
{
    public class SearchResultPage
    {
        public SearchResultPage()
        {
            SearchResultsHistory = new Dictionary<int, List<TweetDto>>();
        }
        public Dictionary<int, List<TweetDto>> SearchResultsHistory { get; set; }
        public string NextResultsParameters { get; set; }
    }

}
