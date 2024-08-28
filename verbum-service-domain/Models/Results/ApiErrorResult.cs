namespace verbum_service_domain.Models.Results
{
    public class ApiErrorResult<T> : ApiResult<T>
    {

        private ApiErrorResult()
        {
            StatusCode = 400;
            IsSuccessed = false;
        }
        private ApiErrorResult(int statusCode, string message)
        {
            StatusCode = statusCode;
            IsSuccessed = false;
            Message = message;
        }
        private ApiErrorResult(string message)
        {
            StatusCode = 400;
            IsSuccessed = false;
            Message = message;
        }

        public static ApiErrorResult<T> Alert(int statusCode, string alertCode, params string[] parameter)
        {
            return new ApiErrorResult<T>(statusCode, string.Format(alertCode, parameter));
        }

        public static ApiErrorResult<T> Alert(int statusCode, string alertCode)
        {
            return new ApiErrorResult<T>(statusCode, string.Format(alertCode));
        }
        public static ApiErrorResult<T> Alert(string alertCode, params string[] parameter)
        {
            return new ApiErrorResult<T>(string.Format(alertCode, parameter));
        }

        public static ApiErrorResult<T> Alert(string alertCode)
        {
            return new ApiErrorResult<T>(string.Format(alertCode));
        }
        public static ApiErrorResult<T> Alert()
        {
            return new ApiErrorResult<T>();
        }
    }
}
