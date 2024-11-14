using ECommerceApp.Business.DTO.Product;

namespace ECommerceApp.Model.Response
{
    public class ProductListResponse
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
