using ECommerceApp.Business.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Business.Contract
{
    public interface IAuthService
    {
        Task<(bool success, string token)> SignInAsync(SignInRequestModel model);
        Task<(bool success, string message)> SignUpAsync(SignUpRequestModel model, IUrlHelper urlHelper, HttpRequest request);
        Task<(bool success, string message)> ConfirmEmailAsync(Guid userId, string token);
    }
}
