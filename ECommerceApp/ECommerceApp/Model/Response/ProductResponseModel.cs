namespace ECommerceApp.Model.Response
{
    public class ProductResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public int TotalRating { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; } = null!;
        public int Rating { get; set; }
        public string Logo { get; set; } = null!;
        public string Background { get; set; } = null!;
        public int Count { get; set; }
    }
}
