using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using ECommerceApp.DAL.Data;
using ECommerceApp.Business.Helper;
using Serilog;
using Serilog.Exceptions;
using E_commerce_Web_Api.Middleware;
using ECommerceApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace E_commerce_Web_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build())
               .Enrich.WithExceptionDetails()
               .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            builder.Services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new {
                            name = e.Key,
                            status = e.Value.Status.ToString(),
                            description = e.Value.Description
                        })
                    });
                    await context.Response.WriteAsync(result);
                }
            });

            app.MapControllers();

            app.Run();
        }
    }
}