using Lombok.NET;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using verbum_service_application.Service;
using verbum_service_domain.Common;
using verbum_service_domain.Models;
using verbum_service_infrastructure.DataContext;

namespace verbum_service_infrastructure.Impl.Service
{
    [RequiredArgsConstructor]
    public partial class CompanyServiceImpl : CompanyService
    {
        private readonly verbumContext context;
        private readonly IHttpContextAccessor httpContext;
        private readonly CurrentUser currentUser;
        public async Task AddCompany(Company company)
        {
            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Guid userId = new Guid(currentUser.Id);
                    context.Companies.Add(company);
                    await context.SaveChangesAsync();
                    context.UserCompanies.Add(new UserCompany
                    {
                        CompanyId = company.CompanyId,
                        UserId = userId
                    });
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
