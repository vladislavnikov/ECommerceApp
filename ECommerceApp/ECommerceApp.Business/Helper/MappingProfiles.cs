using AutoMapper;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.Business.Model.Model;
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

            //Product Maps

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Product, ProductResponseModel>().ReverseMap();

            CreateMap<Product, ProductRequestModel>().ReverseMap();

            CreateMap<ProductDto, ProductResponseModel>().ReverseMap();

            CreateMap<ProductDto, ProductRequestModel>().ReverseMap();

            CreateMap<ProductResponseModel, ProductRequestModel>().ReverseMap();
        }
    }
}
