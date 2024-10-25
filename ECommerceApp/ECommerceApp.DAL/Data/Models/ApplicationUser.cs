using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DAL.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? AddressDelivery { get; set; }
        public DateTime AccountCreationDate { get; set; }
    }
}
