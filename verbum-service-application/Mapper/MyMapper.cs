using AutoMapper;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.Models;

namespace verbum_service_application.Mapper
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<UserSignUp, User>().ReverseMap();
        }
    }
}
