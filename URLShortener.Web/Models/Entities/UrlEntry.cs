
namespace URLShortener.Web.Entities
{
    public class UrlEntry
    {
        public int Id { get; set; }
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int ClickCount { get; set; } 
    }
}
