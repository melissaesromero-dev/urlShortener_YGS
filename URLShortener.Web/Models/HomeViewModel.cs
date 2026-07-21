using System.ComponentModel.DataAnnotations;

namespace URLShortener.Web.Models;

public class HomeViewModel
{
    [Required(ErrorMessage = "You must enter a URL to shorten.")]
    [Url(ErrorMessage = "Please enter a valid URL.")]
    [Display(Name = "Long URL")]
    public string OriginalUrl { get; set; } = string.Empty;

    public string? GeneratedShortUrl { get; set; }

    public string? ShortCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ClickCount { get; set; }
}