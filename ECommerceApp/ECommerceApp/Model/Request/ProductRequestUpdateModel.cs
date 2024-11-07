namespace ECommerceApp.Model.Request
{
    public class ProductRequestUpdateModel 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public decimal Price { get; set; }
        public string Genre { get; set; } = null!;
        public int TotalRating { get; set; }
        public int Rating { get; set; }
        public string Logo { get; set; } = null!;
        public string Background { get; set; } = null!;
        public int Count { get; set; }
    }
}
