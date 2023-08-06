namespace BGCTest.Api.Services.Results
{
    public class ServiceResult : ServiceResult<object>
    {
        public ServiceResult() { }

        public ServiceResult(string error) : base(error) { }

        public ServiceResult(bool isSuccess, object result = null, IEnumerable<string> errors = null)
            : base(isSuccess, result, errors) { }
    }

    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ServiceResult() { }

        public ServiceResult(string error) : this(false, errors: new[] { error }) { }

        public ServiceResult(bool isSuccess, T result = default
            , IEnumerable<string> errors = null)
        {
            IsSuccess = isSuccess;
            Result = result;
            Errors = errors;
        }
    }
}
