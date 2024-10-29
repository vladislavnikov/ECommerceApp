using AutoMapper;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.Business.Model.Request;
using ECommerceApp.Business.Model.Response;
using ECommerceApp.DAL.Data.Models;

namespace ECommerceApp.Business.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, UserUpdateDto>().ReverseMap();

            CreateMap<ApplicationUser, UserInfoDto>().ReverseMap();

            CreateMap<UserUpdateRequest, UserInfoDto>().ReverseMap();

            CreateMap<UserUpdateResponse, UserInfoDto>().ReverseMap();

            CreateMap<PasswordUpdateDto, PasswordUpdateRequest>().ReverseMap();
        }
    }
}
