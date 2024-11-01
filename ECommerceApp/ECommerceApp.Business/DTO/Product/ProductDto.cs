using ECommerceApp.DAL.Data.Models.Enum;

namespace ECommerceApp.Business.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Platform { get; set; }

        public DateTime DateCreated { get; set; }

        public int TotalRating { get; set; }

        public decimal Price { get; set; }
    }
}
