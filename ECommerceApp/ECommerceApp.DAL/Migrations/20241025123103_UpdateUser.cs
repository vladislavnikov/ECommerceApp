using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c9d1fb88-c630-42c6-b46f-cba227bc1866"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("cc7d0784-021c-4178-bff2-5c1d8167cc92"), new Guid("4990875c-cd1d-4270-bc4b-a9936e76f083") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cc7d0784-021c-4178-bff2-5c1d8167cc92"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4990875c-cd1d-4270-bc4b-a9936e76f083"));

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountCreationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AddressDelivery",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AccountCreationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AddressDelivery",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("c9d1fb88-c630-42c6-b46f-cba227bc1866"), null, "User", "USER" },
                    { new Guid("cc7d0784-021c-4178-bff2-5c1d8167cc92"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4990875c-cd1d-4270-bc4b-a9936e76f083"), 0, "04ad67e5-d846-4976-bb74-9ac16376adbb", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEEdoDJa5tYrCiJap8Dvo/Hx38r/C5XZtlsVYqD/W/gbBBdbg3CaFQqRX2Q/gnV1cuA==", null, false, null, false, "AdminUser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("cc7d0784-021c-4178-bff2-5c1d8167cc92"), new Guid("4990875c-cd1d-4270-bc4b-a9936e76f083") });
        }
    }
}
