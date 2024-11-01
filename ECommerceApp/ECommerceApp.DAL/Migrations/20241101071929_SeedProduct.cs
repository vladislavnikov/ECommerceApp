using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApp.DAL.Migrations
{
    public partial class SeedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete old data only if it exists to prevent exceptions
            migrationBuilder.Sql(@"DELETE FROM AspNetRoles WHERE Id IN ('c7d4e77e-3432-402d-9b00-dd2163230825', '8c384343-412d-4aef-a926-c8256bedd30e')");
            migrationBuilder.Sql(@"DELETE FROM AspNetUserRoles WHERE RoleId = '8c384343-412d-4aef-a926-c8256bedd30e' AND UserId = '3cb1f696-980e-4831-850f-b9800ffc6f34'");
            migrationBuilder.Sql(@"DELETE FROM AspNetUsers WHERE Id = '3cb1f696-980e-4831-850f-b9800ffc6f34'");

            // Insert new roles
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1a68da07-a44f-483a-97b8-1b6e1d447ede"), null, "User", "USER" },
                    { new Guid("1bd4c0f5-99fe-46ef-bc39-f1b3bc97ad00"), null, "Admin", "ADMIN" }
                });

            // Insert new user
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountCreationDate", "AddressDelivery", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[]
                {
                    new Guid("4cfd4348-a5c9-47ad-860d-6deb7297b827"), 0, DateTime.UtcNow, null,
                    "3beff870-f36c-4220-80d9-254005b7910b", "admin@mail.com", true, false, null,
                    "ADMIN@MAIL.COM", "ADMINUSER",
                    "AQAAAAIAAYagAAAAEGh987+UHIq6OuL+G7qlwM+UdoWFBh0so301bvhUbtUXHJn7WCE9S6MYAORyZijKfw==",
                    null, false, null, false, "AdminUser"
                });

            // Insert user-role relationship
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1bd4c0f5-99fe-46ef-bc39-f1b3bc97ad00"), new Guid("4cfd4348-a5c9-47ad-860d-6deb7297b827") });

            // Update product creation dates
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: DateTime.UtcNow.AddMonths(-5));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: DateTime.UtcNow.AddMonths(-3));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: DateTime.UtcNow.AddMonths(-2));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: DateTime.UtcNow.AddMonths(-1));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete newly added roles, user, and user roles only if they exist
            migrationBuilder.Sql(@"DELETE FROM AspNetUserRoles WHERE RoleId = '1bd4c0f5-99fe-46ef-bc39-f1b3bc97ad00' AND UserId = '4cfd4348-a5c9-47ad-860d-6deb7297b827'");
            migrationBuilder.Sql(@"DELETE FROM AspNetRoles WHERE Id IN ('1a68da07-a44f-483a-97b8-1b6e1d447ede', '1bd4c0f5-99fe-46ef-bc39-f1b3bc97ad00')");
            migrationBuilder.Sql(@"DELETE FROM AspNetUsers WHERE Id = '4cfd4348-a5c9-47ad-860d-6deb7297b827'");

            // Restore old roles, user, and user roles
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8c384343-412d-4aef-a926-c8256bedd30e"), null, "Admin", "ADMIN" },
                    { new Guid("c7d4e77e-3432-402d-9b00-dd2163230825"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountCreationDate", "AddressDelivery", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[]
                {
                    new Guid("3cb1f696-980e-4831-850f-b9800ffc6f34"), 0, DateTime.UtcNow, null,
                    "efd91fa6-596d-455e-acf8-fe8a64320041", "admin@mail.com", true, false, null,
                    "ADMIN@MAIL.COM", "ADMINUSER",
                    "AQAAAAIAAYagAAAAEHsr/eWZ9RdQLG7WrYqSlONMGWSvy3MfTs7/V0dciK/6S4RL+OGqLWWNSZGbsXWy/A==",
                    null, false, null, false, "AdminUser"
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8c384343-412d-4aef-a926-c8256bedd30e"), new Guid("3cb1f696-980e-4831-850f-b9800ffc6f34") });
        }
    }
}
