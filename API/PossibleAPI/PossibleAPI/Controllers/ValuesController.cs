using Microsoft.AspNetCore.Mvc;
using PossibleAPI.Models;
using Services;
using System.Collections.Generic;

namespace PossibleAPI.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IUserLastTweetService _userLastTweetService;

        public ValuesController(ISearchService searchService, IUserLastTweetService userLastTweetService)
        {
            _searchService = searchService; //ToDo: DI
            _userLastTweetService = userLastTweetService;
        }

        // GET api/values
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IEnumerable<TweetModel>> Get()
        {
            var res = _userLastTweetService.UserLastTweet("");
            return res.ConvertAll(dto => new TweetModel(dto));
        }

        // POST api/values
        [HttpPost]
        public ActionResult<IEnumerable<TweetModel>> Post([FromBody] SearchModel searchModel)
        {
            if (searchModel != null)
            {
                var res = _searchService.Search(searchModel.SearchText, searchModel.Page);
                return res.ConvertAll(dto => new TweetModel(dto));
            }
            return BadRequest("SearchModel is not valid");
        }
    }
}
