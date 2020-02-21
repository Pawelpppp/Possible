namespace Services.Dto
{
    public class TweetDto
    {
        public string Date { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }

        public TweetDto(string date, string text, string name)
        {
            Date = date;
            Text = text;
            Name = name;
        }
    }
}
