using verbum_service_domain.Models;

namespace verbum_service_application.Service
{
    public interface CompanyService
    {
        Task AddCompany(Company company);
    }
}
