using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using URLShortener.Web.Data;
using URLShortener.Web.Entities;

namespace URLShortener.Web.Services
{
    public class UrlShortenerService
    {
        private readonly ApplicationDbContext _context;

        //Allowed characters and allowed code length
        private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int ShortCodeLength = 6;

        public UrlShortenerService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create and save a shortened URL
        public async Task<UrlEntry> ShortenUrlAsync(string originalUrl)
        {
            #region VALIDATE URL

            //Check URL is not empty
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                throw new ArgumentException(
                    "You must enter a URL to shorten.");
            }

            originalUrl = originalUrl.Trim();

            //Check URL is valid and uses HTTP or HTTPS
            if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp &&
                 uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException(
                    "Please enter a valid HTTP or HTTPS URL.");
            }

            #endregion

            #region GENERATE SHORT CODE

            string shortenedCode;

            //Generate another code if it already exists
            do
            {
                shortenedCode = GenerateShortCode();
            }
            while (await _context.Urls.AnyAsync(
                url => url.ShortUrl == shortenedCode));

            #endregion

            #region CREATE URL ENTRY

            var urlEntry = new UrlEntry
            {
                OriginalUrl = originalUrl,
                ShortUrl = shortenedCode
            };
            #endregion

            #region SAVE URL ENTRY

            _context.Urls.Add(urlEntry);
            await _context.SaveChangesAsync();

            #endregion

            return urlEntry;
        }

        // Find the original URL and record a click
        public async Task<string?> ResolveUrlAsync(string shortUrl)
        {
            #region FIND URL

            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                return null;
            }

            var urlEntry = await _context.Urls
                .Where(url => url.ShortUrl == shortUrl)
                .Select(url => new
                {
                    url.Id,
                    url.OriginalUrl
                })
                .FirstOrDefaultAsync();

            if (urlEntry == null)
            {
                return null;
            }

            #endregion

            #region UPDATE CLICK COUNT

            await _context.Urls
                .Where(url => url.Id == urlEntry.Id)
                .ExecuteUpdateAsync(update =>
                    update.SetProperty(
                        url => url.ClickCount,
                        url => url.ClickCount + 1));

            #endregion

            return urlEntry.OriginalUrl;
        }

        // Retrieve analytics for a short URL
        public async Task<UrlEntry?> GetAnalyticsAsync(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                return null;
            }

            var urlEntry = await _context.Urls
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    url => url.ShortUrl == shortUrl);

            return urlEntry;
        }

        // Generate a random short code
        private string GenerateShortCode()
        {
            var newCode = new char[ShortCodeLength];

            for (var i = 0; i < ShortCodeLength; i++)
            {
                var randomIndex = RandomNumberGenerator.GetInt32(AllowedCharacters.Length, i);

                newCode[i] = AllowedCharacters[randomIndex];
            }

            return new string(newCode); 
        }
    }
}
