using Microsoft.AspNetCore.Mvc;
using PossibleAPI.Models;
using Services;
using Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public ActionResult<IEnumerable<TweetDto>> Post([FromBody] SearchModel searchModel)
        {
            if (searchModel != null)
            {
                var res = _searchService.Search(searchModel.SearchText, searchModel.Page);
                return res;
            }
            return new JsonResult("Dupa");
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
