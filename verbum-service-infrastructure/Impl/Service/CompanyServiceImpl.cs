using Lombok.NET;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using verbum_service_application.Service;
using verbum_service_domain.DTO.Request;
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
