using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.DAL.Data.Models.Enum;
using ECommerceApp.Model.Request;

public class ValidateProductListFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.ActionArguments.Values.OfType<ProductListRequest>().FirstOrDefault();

        if (request != null)
        {
            if (!string.IsNullOrEmpty(request.SortBy) && !new[] { "Rating", "Price" }.Contains(request.SortBy))
            {
                context.Result = new BadRequestObjectResult("Invalid SortBy parameter.");
                return;
            }

            if (!string.IsNullOrEmpty(request.Order) && !new[] { "asc", "desc" }.Contains(request.Order.ToLower()))
            {
                context.Result = new BadRequestObjectResult("Invalid Order parameter.");
                return;
            }

            if (!string.IsNullOrEmpty(request.Age) && !Enum.TryParse<Rating>(request.Age, out _))
            {
                context.Result = new BadRequestObjectResult("Invalid Age parameter.");
                return;
            }
        }

        base.OnActionExecuting(context);
    }
}
