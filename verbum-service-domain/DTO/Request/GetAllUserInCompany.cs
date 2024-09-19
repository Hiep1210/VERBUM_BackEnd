namespace verbum_service_domain.DTO.Request
{
    public class GetAllUserInCompany
    {
        public string? searchTerm { get; set; }
        public string? roleTerm { get; set; }
        public string? statusTerm { get; set; }
        public string? sortColumn { get; set; }
        public string? sortOrder { get; set; }
        public int? page {  get; set; }
        public int? pageSize { get; set; }
    }
}
