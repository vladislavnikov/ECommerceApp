﻿using Serilog;
using System.Text.Json;

namespace ECommerceApp.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unhandled exception.");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    context.Response.StatusCode,
                    Message = "An internal server error."
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
