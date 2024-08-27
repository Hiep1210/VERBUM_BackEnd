namespace verbum_service_domain.Models.Results
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }
        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}
