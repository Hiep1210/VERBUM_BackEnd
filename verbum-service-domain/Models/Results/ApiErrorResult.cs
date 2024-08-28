namespace verbum_service_domain.Models.Results
{
    public class ApiErrorResult<T> : ApiResult<T>
    {

        private ApiErrorResult()
        {
            IsSuccessed = false;
        }
        private ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
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
