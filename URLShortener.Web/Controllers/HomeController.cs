using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Web.Models;
using URLShortener.Web.Services;

namespace URLShortener.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UrlShortenerService _urlShortenerService;

        public HomeController(
            ILogger<HomeController> logger,
            UrlShortenerService urlShortenerService)
        {
            _logger = logger;
            _urlShortenerService = urlShortenerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                GeneratedShortUrl = TempData["GeneratedShortUrl"] as string
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var urlEntry = await _urlShortenerService
                    .ShortenUrlAsync(model.OriginalUrl);

                TempData["GeneratedShortUrl"] = 
                    $"{Request.Scheme}://{Request.Host}/{urlEntry.ShortUrl}";

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException exception)
            {
                ModelState.AddModelError(
                    nameof(model.OriginalUrl),
                    exception.Message);

                return View(model);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
