namespace URLShortener.Web.Models
{
    public class UrlAnalyticsResponse
    {
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ClickCount { get; set; }
    }
}
