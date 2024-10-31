using ECommerceApp.DAL.Data.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.DAL.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Platforms Platform { get; set; }

        public DateTime DateCreated { get; set; }

        public int TotalRating { get; set; }

        public decimal Price { get; set; }
    }
}
