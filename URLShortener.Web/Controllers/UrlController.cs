using Microsoft.AspNetCore.Mvc;
using URLShortener.Web.Models;
using URLShortener.Web.Services;

namespace URLShortener.Web.Controllers
{
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlShortenerService _urlShortenerService;

        public UrlController(UrlShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
        }

        // Create a shortened URL
        [HttpPost("/shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] ShortenUrlRequest request)
        {
            try
            {
                var urlEntry =
                    await _urlShortenerService
                        .ShortenUrlAsync(request.OriginalUrl);

                var completeShortUrl = $"{Request.Scheme}://{Request.Host}/{urlEntry.ShortUrl}";

                return Ok(new
                {
                    shortUrl = completeShortUrl
                });
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new
                {
                    error = exception.Message
                });
            }
        }

        // Redirect to the original URL
        [HttpGet("/{shortUrl:length(6)}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl)
        {
            var originalUrl =
                await _urlShortenerService
                    .ResolveUrlAsync(shortUrl);

            if (originalUrl == null)
            {
                return NotFound(new
                {
                    error = "Short URL was not found."
                });
            }

            return Redirect(originalUrl);
        }

        // Get analytics for a shortened URL
        [HttpGet("/analytics/{shortUrl:length(6)}")]
        public async Task<IActionResult> GetAnalytics(string shortUrl)
        {
            var urlEntry =
                await _urlShortenerService
                    .GetAnalyticsAsync(shortUrl);

            if (urlEntry == null)
            {
                return NotFound(new
                {
                    error = "Short URL was not found."
                }); 
            }

            var response = new UrlAnalyticsResponse
            {
                OriginalUrl = urlEntry.OriginalUrl,
                ShortUrl = urlEntry.ShortUrl,
                CreatedAt = urlEntry.CreatedAt,
                ClickCount = urlEntry.ClickCount
            };

            return Ok(response);
        }
    }
}
