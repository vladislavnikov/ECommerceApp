using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Business.DTO.Product
{
    public class ProductListDto
    {
        public string Genre { get; set; }
        public string Age { get; set; }
        public string SortBy { get; set; }
        public string Order { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int TotalItems { get; set; }
    }
}
