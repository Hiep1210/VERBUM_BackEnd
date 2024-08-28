namespace verbum_service_domain.Models.Results
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        private ApiSuccessResult(int statusCode, T resultObj)
        {
            StatusCode = statusCode;
            IsSuccessed = true;
            ResultObj = resultObj;
        }
        private ApiSuccessResult(T resultObj)
        {
            StatusCode = 200;
            IsSuccessed = true;
            ResultObj = resultObj;
        }
        private ApiSuccessResult()
        {
            IsSuccessed = true;
            StatusCode = 200;
        }
        public static ApiSuccessResult<T> Success(int statusCode, T resultObj)
        {
            return new ApiSuccessResult<T>(statusCode, resultObj);
        }
        public static ApiSuccessResult<T> Success()
        {
            return new ApiSuccessResult<T>();
        }
        public static ApiSuccessResult<T> Success(T resultObj)
        {
            return new ApiSuccessResult<T>(resultObj);
        }
    }
}
