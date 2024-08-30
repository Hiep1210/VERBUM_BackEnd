using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_application.Service;
using verbum_service_application.Workflow;
using verbum_service_domain.Common;
using verbum_service_domain.Common.ErrorModel;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;
using verbum_service_domain.Utils;
using verbum_service_infrastructure.Impl.Validation;

namespace verbum_service_infrastructure.Impl.Workflow
{
    public class CreateUserWorkflow : AbstractWorkFlow<UserSignUp>
    {
        private readonly IMapper mapper;
        private readonly UserSignUpValidation validation;
        private readonly UserService userService;
        private User user = new User();
        private Tokens tokens = new Tokens();
        public CreateUserWorkflow(IMapper mapper, UserSignUpValidation validation, UserService userService)
        {
            this.mapper = mapper;
            this.validation = validation;
            this.userService = userService;
        }
        protected override async Task PreStep(UserSignUp request)
        {
            Console.WriteLine("start user workflow");
            //verified email
        }
        protected override async Task ValidationStep(UserSignUp request)
        {
            List<string> alerts = await validation.Validate(request);
            if (ObjectUtils.IsNotEmpty(alerts))
            {
                throw new BusinessException(alerts);
            }
        }
        protected override async Task CommonStep(UserSignUp request)
        {
            user = mapper.Map<User>(request);
            user.Id = Guid.NewGuid();
            user.EmailVerified = DateTime.Now;
            //may need refactor for gg sign in (no password in request
            user.Password = UserUtils.HashPassword(request.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.Status = UserStatus.ACTIVE.ToString();
        }

        protected override async Task PostStep(UserSignUp request)
        {
            tokens = await userService.SignUp(user);
        }
        public Tokens GetResponse()
        {
            return tokens;
        }
    }
}
