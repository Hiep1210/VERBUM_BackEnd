using AutoMapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_application.Service;
using verbum_service_application.Workflow;
using verbum_service_domain.Common.ErrorModel;
using verbum_service_domain.Common;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;
using verbum_service_domain.Utils;
using verbum_service_infrastructure.Impl.Validation;

namespace verbum_service_infrastructure.Impl.Workflow
{
    public class UpdateUserWorkflow : AbstractWorkFlow<UserUpdate>
    {
        private readonly IMapper mapper;
        private readonly UserUpdateValidation validation;
        private readonly UserService userService;
        public UpdateUserWorkflow(IMapper mapper, UserUpdateValidation validation, UserService userService)
        {
            this.mapper = mapper;
            this.validation = validation;
            this.userService = userService;
        }

        protected override async Task PreStep(UserUpdate request)
        {
        }
        protected override async Task ValidationStep(UserUpdate request)
        {
            List<string> alerts = await validation.Validate(request);
            if (ObjectUtils.IsNotEmpty(alerts))
            {
                throw new BusinessException(alerts);
            }
        }
        protected override async Task CommonStep(UserUpdate request)
        {
        }

        protected override async Task PostStep(UserUpdate request)
        {
            await userService.UpdateUser(request);
        }
        public string GetResponse()
        {
            return "Update user success";
        }
    }
}
