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
        private readonly SearchService _searchService;
        private readonly UserLastTweetService _userLastTweetService;

        public ValuesController()
        {
            _searchService = new SearchService(); //ToDo: DI
            _userLastTweetService = new UserLastTweetService();
        }

        // GET api/values
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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

        // PUT api/values/5
        [HttpPut("{id}")]
        [Produces("application/json")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
