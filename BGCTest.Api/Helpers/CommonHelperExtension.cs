namespace BGCTest.Api.Helpers
{
    public static class CommonHelperExtension
    {
        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);

        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int limit, int pageNumber)
            where T : class
        {
            if (limit < 1)
            {
                limit = 20;
            }
            int skip = ((pageNumber < 1 ? 1 : pageNumber) - 1) * limit;
            return query.Skip(skip).Take(limit);
        }
    }
}
