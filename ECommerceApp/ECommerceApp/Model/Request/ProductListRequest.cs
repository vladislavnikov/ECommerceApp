using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Model.Request
{
    public class ProductListRequest
    {
        public string Genre { get; set; }
        public string Age { get; set; }
        public string SortBy { get; set; }
        public string Order { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
