namespace verbum_service_domain.Models.Results
{
    public class ApiErrorResult<T> : ApiResult<T>
    {

        public ApiErrorResult()
        {
            IsSuccessed = false;
        }
        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
    }
}
