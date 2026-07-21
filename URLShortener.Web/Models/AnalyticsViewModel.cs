using URLShortener.Web.Entities;

namespace URLShortener.Web.Models
{
    public class AnalyticsViewModel
    {
        // All shortened URLs displayed in the list
        public List<UrlEntry> Urls { get; set; } = new();

        // The URL currently selected by the user
        public UrlEntry? SelectedUrl { get; set; }
    }
}