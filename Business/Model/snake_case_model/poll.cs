using System.Collections.Generic;
using System;

namespace Business.Model.snake_case_model
{
    public class poll
    {
        public int id { get; set; }
        public string question { get; set; }
        public string image_url { get; set; }
        public string thumb_url { get; set; }
        public DateTime published_at { get; set; }
        public List<option> choices { get; set; } = new List<option>();
    }
}
