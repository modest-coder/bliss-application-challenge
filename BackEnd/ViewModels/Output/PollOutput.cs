using System.Collections.Generic;
using System;

namespace API.ViewModels.Output
{
    public class PollOutput
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }
        public DateTime PublishedAt { get; set; }
        public List<OptionOutput> Choices { get; set; }
    }
}
