using System.Collections.Generic;
using Services.Dto;

namespace Services
{
    public interface IUserLastTweetService
    {
        List<TweetDto> UserLastTweet(string name);
    }
}