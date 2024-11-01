namespace ECommerceApp.Business.Model.Model
{
    public class ProductResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Platform { get; set; }

        public DateTime DateCreated { get; set; }

        public int TotalRating { get; set; }

        public decimal Price { get; set; }
    }
}
