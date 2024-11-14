using ECommerceApp.DAL.Data.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DAL.Data.Models
{
    public class Product
    {
        public Product()
        {
            Ratings = new List<ProductRating>();
        }
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Platforms Platform { get; set; }

        public DateTime DateCreated { get; set; }

        public int TotalRating { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public Rating Rating { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        public string Background { get; set; }

        [Required]
        public int Count { get; set; }

        public ICollection<ProductRating> Ratings { get; set; }
    }
}
