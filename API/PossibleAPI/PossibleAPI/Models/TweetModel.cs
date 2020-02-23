using Services.Dto;

namespace PossibleAPI.Models
{
    public class TweetModel
    {
        public string Date { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }

        public TweetModel(TweetDto dto)
        {
            Date = dto.Date;
            Text = dto.Text;
            Name = dto.Name;
        }
    }
}
