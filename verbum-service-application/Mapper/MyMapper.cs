using AutoMapper;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;

namespace verbum_service_application.Mapper
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<UserSignUp, User>().ReverseMap();
            CreateMap<UserCompany, UserInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Relevancy, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserCompanyStatus, opt => opt.MapFrom(src => src.Status))
                ;
        }
    }
}
