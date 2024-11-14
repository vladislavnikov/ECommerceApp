using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProductRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ee7784cd-5632-4d68-9f40-72f1f5599326"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("701711d2-53de-40b4-95c4-5d9e2611394d"), new Guid("23707d8d-7ac0-44f0-b468-486b4654cd94") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("701711d2-53de-40b4-95c4-5d9e2611394d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("23707d8d-7ac0-44f0-b468-486b4654cd94"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5c6a0cd0-f16e-43e4-bdee-1a7d2f5458ac"), null, "User", "USER" },
                    { new Guid("90323052-4552-4084-9ce9-39ce9f4ee136"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountCreationDate", "AddressDelivery", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("09eeefa3-6bba-4e64-bceb-c109f5ab7a63"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "eb1d46eb-0b21-46b0-a1a1-26e6bf9ff08f", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMINUSER", "AQAAAAIAAYagAAAAECnCC4VJkjHr7MBdT+iaUluAXFSrG5UqbcB5oVKozY36WrNY5YbF6+M2pLICl+vZ5A==", null, false, null, false, "AdminUser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("90323052-4552-4084-9ce9-39ce9f4ee136"), new Guid("09eeefa3-6bba-4e64-bceb-c109f5ab7a63") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5c6a0cd0-f16e-43e4-bdee-1a7d2f5458ac"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("90323052-4552-4084-9ce9-39ce9f4ee136"), new Guid("09eeefa3-6bba-4e64-bceb-c109f5ab7a63") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("90323052-4552-4084-9ce9-39ce9f4ee136"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09eeefa3-6bba-4e64-bceb-c109f5ab7a63"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("701711d2-53de-40b4-95c4-5d9e2611394d"), null, "Admin", "ADMIN" },
                    { new Guid("ee7784cd-5632-4d68-9f40-72f1f5599326"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountCreationDate", "AddressDelivery", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("23707d8d-7ac0-44f0-b468-486b4654cd94"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "0c2ae237-5e30-4505-8194-842d3dc3fef1", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEL7Y21uBPQqQP3/NcSokcY1RBQdgikScGdyyanheZyWQHdEXFPTJbQLTgYlB0H5hlA==", null, false, null, false, "AdminUser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("701711d2-53de-40b4-95c4-5d9e2611394d"), new Guid("23707d8d-7ac0-44f0-b468-486b4654cd94") });
        }
    }
}
