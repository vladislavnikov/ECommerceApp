using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DAL.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Ratings = new List<ProductRating>();
        }
        public string? AddressDelivery { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
