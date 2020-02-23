namespace TwitterManager
{
    public interface ITwitterRequest
    {
        string Search(string searchText);
        string Search(string searchText, string parameters);
        string UserLastTweet();
    }
}