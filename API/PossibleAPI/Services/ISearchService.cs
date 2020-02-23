using System.Collections.Generic;
using Services.Dto;

namespace Services
{
    public interface ISearchService
    {
        List<TweetDto> Search(string search, int page);
    }
}