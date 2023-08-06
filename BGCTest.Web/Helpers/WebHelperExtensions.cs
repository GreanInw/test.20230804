namespace BGCTest.Web.Helpers
{
    public static class WebHelperExtensions
    {
        public static string ToGBDateString(this DateTime date, string format)
            => date.ToString(format, WebHelpers.GBCulture);
    }
}