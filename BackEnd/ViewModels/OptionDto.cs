using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class OptionDto
    {
        [Required]
        public string Choice { get; set; }
        [Required]
        public int Votes { get; set; }
    }
}
