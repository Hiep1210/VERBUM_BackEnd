namespace verbum_service_domain.Models.Results
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? ResultObj { get; set; }
    }
}
