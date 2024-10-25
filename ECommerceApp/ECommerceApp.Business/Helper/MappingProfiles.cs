using AutoMapper;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.DAL.Data.Models;

namespace ECommerceApp.Business.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, UserUpdateDto>().ReverseMap();
        }
    }
}
