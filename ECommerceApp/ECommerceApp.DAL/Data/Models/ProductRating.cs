namespace ECommerceApp.DAL.Data.Models
{
    public class ProductRating
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Rating { get; set; }
    }
}
