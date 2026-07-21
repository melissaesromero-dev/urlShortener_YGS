using Microsoft.AspNetCore.Mvc;
using URLShortener.Web.Models;
using URLShortener.Web.Services;

namespace URLShortener.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly UrlShortenerService _urlShortenerService;

        public AnalyticsController(
            UrlShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? shortCode)
        {
            var model = new AnalyticsViewModel
            {
                Urls = await _urlShortenerService.GetAllUrlsAsync()
            };

            if (!string.IsNullOrWhiteSpace(shortCode))
            {
                model.SelectedUrl = await _urlShortenerService
                    .GetAnalyticsAsync(shortCode);
            }

            return View(model);
        }
    }
}