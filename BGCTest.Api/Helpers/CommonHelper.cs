namespace BGCTest.Api.Helpers
{
    public class CommonHelper
    {
        public static int GetNextPageNumber(int totalRow, int limit, int pageNumber) 
            => totalRow < limit ? -1 : pageNumber + 1;
    }
}