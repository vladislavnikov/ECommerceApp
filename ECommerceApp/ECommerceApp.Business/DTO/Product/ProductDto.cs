using ECommerceApp.DAL.Data.Models.Enum;

namespace ECommerceApp.Business.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Platform { get; set; }

        public DateTime DateCreated { get; set; }

        public int TotalRating { get; set; }

        public decimal Price { get; set; }

        public string Genre { get; set; }

        public char Rating { get; set; }

        public string Logo { get; set; }

        public string Background { get; set; }

        public int Count { get; set; }
    }
}
