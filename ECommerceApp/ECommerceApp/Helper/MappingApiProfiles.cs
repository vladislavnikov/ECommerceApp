using AutoMapper;
using ECommerceApp.Business.DTO.Product;
using ECommerceApp.Business.DTO.ProductRating;
using ECommerceApp.Business.DTO.User;
using ECommerceApp.Model.Request;
using ECommerceApp.Model.Response;

namespace ECommerceApp.Helper
{
    public class MappingApiProfiles : Profile
    {
        public MappingApiProfiles()
        {
            CreateMap<UserUpdateRequest, UserInfoDto>().ReverseMap();

            CreateMap<UserUpdateResponse, UserInfoDto>().ReverseMap();

            CreateMap<PasswordUpdateDto, PasswordUpdateRequest>().ReverseMap();

            CreateMap<ProductDto, ProductResponseModel>().ReverseMap();

            CreateMap<ProductRequestModel, ProductDto>().ReverseMap();

            CreateMap<ProductRequestUpdateModel, ProductDto>().ReverseMap();

            CreateMap<ProductRatingDto, RateProductRequestModel>();

            CreateMap<ProductListRequest, ProductListDto>();

            CreateMap<ProductListDto, ProductListResponse>();
        }
    }
}
