using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace API.ViewModels
{
    public class PollDto
    {
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string ThumbUrl { get; set; }
        [Required]
        public DateTime PublishedAt { get; set; }

        public List<OptionDto> Choices { get; set; }
    }
}
