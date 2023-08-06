namespace BGCTest.Api.DTOs.Bases
{
    public abstract class PagingData : IPaging
    {
        private int _limit;
        private int _pageNumber;

        public int Limit { get => _limit < 1 ? 50 : _limit; set => _limit = value; }
        public int PageNumber { get => _pageNumber < 1 ? 1 : _pageNumber; set => _pageNumber = value; }
    }
}
