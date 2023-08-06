using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace BGCTest.Web.Helpers
{
    public class WebHelpers
    {
        public static CultureInfo GBCulture => CultureInfo.GetCultureInfo("en-GB");
        public static DateTime FirstDate() => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        public static IEnumerable<string> Regions => new[]
        {
            "North", "South", "East", "West",
            "Northeast", "Southeast", "Northwest", "Southwest"
        };

        public static IEnumerable<SelectListItem> SelectItemRegions
            => Regions.Select(s => new SelectListItem(s, s));
    }
}