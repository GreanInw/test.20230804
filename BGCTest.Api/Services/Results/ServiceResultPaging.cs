using BGCTest.Api.DTOs.Bases;

namespace BGCTest.Api.Services.Results
{
    public class ServiceResultPaging<T> : ServiceResult<IEnumerable<T>>, IPaging
    {
        public ServiceResultPaging() { }

        public ServiceResultPaging(bool isSuccess, IEnumerable<T> result = null, IEnumerable<string> errors = null)
            : base(isSuccess, result, errors)
        { }

        public ServiceResultPaging(bool isSuccess, int limit = -1, int pageNumber = -1
            , IEnumerable<T> result = null, IEnumerable<string> errors = null)
            : base(isSuccess, result, errors)
        {
            Limit = limit;
            PageNumber = pageNumber;
        }

        public int Limit { get; set; }
        public int PageNumber { get; set; }

    }
}
