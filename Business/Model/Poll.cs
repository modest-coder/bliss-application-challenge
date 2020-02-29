using System.Collections.Generic;
using System;

namespace Business.Model
{
    public class Poll
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }
        public DateTime PublishedAt { get; set; }
        public List<Option> Choices { get; set; } = new List<Option>();
    }
}
