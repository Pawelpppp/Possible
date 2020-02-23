using Newtonsoft.Json.Linq;
using Services.Dto;
using Services.Extentions;
using System;
using System.Collections.Generic;
using TwitterManager;

namespace Services
{
    public class SearchService
    {
        private static Dictionary<string, SearchResultPage> PaginationDictionary = new Dictionary<string, SearchResultPage>();
        private TwitterRequest _twitterRequest;
        public SearchService()
        {
            _twitterRequest = new TwitterRequest();
        }
        
        public List<TweetDto> Search(string search, int page)
        {
            SearchResultPage searchResult = null;
            if (PaginationDictionary.TryGetValue(search, out searchResult))
            {
                // return previus result
                List<TweetDto> resultList;
                if (searchResult.SearchResultsHistory.TryGetValue(page, out resultList))
                {
                    return resultList;
                }

                // chceck if we have pagination link
                if (!string.IsNullOrEmpty(searchResult.NextResultsParameters))
                {
                    return SearchWithPagination(search, page, searchResult.NextResultsParameters);
                }
            }

            // first time serch
            if (page == 1)
            {
                return SearchFirstTime(search, page);
            }
            return null;
        }

        private List<TweetDto> SearchWithPagination(string search, int page, string next)
        {
            string searchResult = _twitterRequest.Search(search, next);

            return GetSearchResult(searchResult, search, page);
        }

        private List<TweetDto> SearchFirstTime(string search, int page)
        {
            //clear dictionary 
            PaginationDictionary.Clear();
            var searchResult = _twitterRequest.Search(search);

            return GetSearchResult(searchResult, search, page);
        }

        private List<TweetDto> GetSearchResult(string searchText, string search, int page)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                dynamic data = JObject.Parse(searchText);
                var result = GetRequiredData(data.statuses);
                TrySavePagination(search, data, page, result);

                return result;
            }
            return null;
        }

        private void TrySavePagination(string search, dynamic data, int page, List<TweetDto> results)
        {
            var next_results = Convert.ToString(data.search_metadata["next_results"]);
            if (!string.IsNullOrEmpty(next_results))
            {
                SearchResultPage pagination;
                if (!PaginationDictionary.TryGetValue(search, out pagination))
                {
                    pagination = new SearchResultPage();
                    PaginationDictionary.Add(search, pagination);
                }

                pagination.SearchResultsHistory.CreateNewOrUpdateExisting(page, results);
                pagination.NextResultsParameters = next_results;
            }

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
