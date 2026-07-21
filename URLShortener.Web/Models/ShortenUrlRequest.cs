using System.ComponentModel.DataAnnotations;

namespace URLShortener.Web.Models
{
    public class ShortenUrlRequest
    {
        [Required(ErrorMessage = "You must enter a URL to shorten.")]
        public string OriginalUrl { get; set; } = string.Empty;
    }
}
