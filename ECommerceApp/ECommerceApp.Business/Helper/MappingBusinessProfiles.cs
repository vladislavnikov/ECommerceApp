using AutoMapper;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.DAL.Data.Models;
using ECommerceApp.DAL.Data.Models.Enum;

namespace ECommerceApp.Business.Helper
{
    public class MappingBusinessProfiles : Profile
    {
        public MappingBusinessProfiles()
        {
            CreateMap<ApplicationUser, UserUpdateDto>().ReverseMap();

            CreateMap<ApplicationUser, UserInfoDto>().ReverseMap();

            //Product Maps

            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform.ToString()))  
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => (int)src.Rating)) 
            .ReverseMap()
            .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => Enum.Parse<Platforms>(src.Platform))) 
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => (Rating)src.Rating));

            CreateMap<ProductRating, ProductUpdateDto>().ReverseMap();


        }
    }
}
