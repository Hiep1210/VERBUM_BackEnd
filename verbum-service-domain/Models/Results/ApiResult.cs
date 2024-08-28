namespace verbum_service_domain.Models.Results
{
    public class ApiResult<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccessed { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? ResultObj { get; set; }
    }
}
