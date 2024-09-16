using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using verbum_service_application.Service;
using verbum_service_domain.Models;
using verbum_service_domain.Utils;

namespace verbum_service.Filter
{
    public class PermissionAuthAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string entity;
        private readonly string action;
        public PermissionAuthAttribute(string entity, string action)
        {
            this.entity = entity;
            this.action = action;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var permissionService = context.HttpContext.RequestServices.GetService(typeof(PermissionService)) as PermissionService;
            if (permissionService == null)
            {
                context.Result = new ForbidResult();
                return;
            }
            List<Permission> permissions = permissionService.GetPermissionsForUser(Guid.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(context.HttpContext.Request.Query["companyId"])).Result;
            if (!ObjectUtils.IsEmpty(permissions.FirstOrDefault(x => x.PermissionName == entity && x.Action == action)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
