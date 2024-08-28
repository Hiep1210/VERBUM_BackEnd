namespace verbum_service_domain.Models.Results
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        private ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }
        private ApiSuccessResult()
        {
            IsSuccessed = true;
        }
        public static ApiSuccessResult<T> Success(T resultObj)
        {
            return new ApiSuccessResult<T>(resultObj);
        }
        public static ApiSuccessResult<T> Success()
        {
            return new ApiSuccessResult<T>();
        }
    }
}
