using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Input
{
    public class ShareInput
    {
        [EmailAddress]
        [Required]
        public string DestinationEmail { get; set; }
        [Required]
        public string ContentUrl { get; set; }
    }
}
