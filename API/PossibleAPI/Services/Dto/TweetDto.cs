using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dto
{
    public class TweetDto
    {
        private dynamic date;
        private dynamic text;
        private dynamic name;

        public TweetDto(dynamic date, dynamic text, dynamic name)
        {
            this.date = date;
            this.text = text;
            this.name = name;
        }
    }
}
