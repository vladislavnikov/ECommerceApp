using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("279adcfd-0d4a-467c-b616-8f25fb3c2580"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d36eba0a-9345-4218-9c19-4dcfb2c1f757"), new Guid("4667e36f-ca17-4ffe-b876-f17dba4b8d8a") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d36eba0a-9345-4218-9c19-4dcfb2c1f757"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4667e36f-ca17-4ffe-b876-f17dba4b8d8a"));

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalRating = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4134e550-2acd-47ee-8d8f-4a0bab5b99dd"), null, "Admin", "ADMIN" },
                    { new Guid("6c73e0bb-3439-4eeb-9a5b-7e39ce55f86a"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountCreationDate", "AddressDelivery", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8037f0c1-d38d-42b2-a123-b510616c4a42"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2eb5e550-9bc2-40de-b216-ed308fca9019", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEIqD23lNRi0XLc1mR7BSMcssPDRAiSWnKtLASnlG2rGoyy8vifoYZq73/EKEhPMKvQ==", null, false, null, false, "AdminUser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("4134e550-2acd-47ee-8d8f-4a0bab5b99dd"), new Guid("8037f0c1-d38d-42b2-a123-b510616c4a42") });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DateCreated",
                table: "Products",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Platform",
                table: "Products",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TotalRating",
                table: "Products",
                column: "TotalRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6c73e0bb-3439-4eeb-9a5b-7e39ce55f86a"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("4134e550-2acd-47ee-8d8f-4a0bab5b99dd"), new Guid("8037f0c1-d38d-42b2-a123-b510616c4a42") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4134e550-2acd-47ee-8d8f-4a0bab5b99dd"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8037f0c1-d38d-42b2-a123-b510616c4a42"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("279adcfd-0d4a-467c-b616-8f25fb3c2580"), null, "User", "USER" },
                    { new Guid("d36eba0a-9345-4218-9c19-4dcfb2c1f757"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountCreationDate", "AddressDelivery", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4667e36f-ca17-4ffe-b876-f17dba4b8d8a"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "aa570f88-b0c8-4dfe-b31a-d5bc560fff65", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEPpeovGh+ihlGvjm1jtxSlwrfojMAUJNHQJ+FRE4MIk911r2fLYzSOaoCKcAVbRvXA==", null, false, null, false, "AdminUser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d36eba0a-9345-4218-9c19-4dcfb2c1f757"), new Guid("4667e36f-ca17-4ffe-b876-f17dba4b8d8a") });
        }
    }
}
