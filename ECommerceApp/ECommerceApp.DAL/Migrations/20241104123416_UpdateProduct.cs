using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a68da07-a44f-483a-97b8-1b6e1d447ede"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1bd4c0f5-99fe-46ef-bc39-f1b3bc97ad00"), new Guid("4cfd4348-a5c9-47ad-860d-6deb7297b827") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1bd4c0f5-99fe-46ef-bc39-f1b3bc97ad00"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4cfd4348-a5c9-47ad-860d-6deb7297b827"));

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1bd877b3-2972-4acb-9de9-dba69c94618b"), null, "User", "USER" },
                    { new Guid("ba0c5c67-0bc8-4f34-9fa4-1774f7df8dc1"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountCreationDate", "AddressDelivery", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("64ee9a6f-2f2f-4a05-8796-c688a8eee9c3"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ce26e8ab-cfe4-42aa-b802-b55e8270dea4", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEOvnD8IzwRfaHRW+icUhnFJCqKSjLplm0cMjLIoNhDvGjFBMpHCkmip/ZolOTpjJww==", null, false, null, false, "AdminUser" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Background", "Count", "DateCreated", "Genre", "Logo", "Name", "Price", "Rating", "TotalRating" },
                values: new object[] { "https://img.freepik.com/free-photo/cyberpunk-urban-scenery_23-2150712464.jpg", 150, new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action RPG", "https://mir-s3-cdn-cf.behance.net/project_modules/1400/81a4e680815973.5cec6bcf6aa1a.jpg", "Cyberpunk 2077", 59.99m, 18, 78 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Background", "Count", "DateCreated", "Genre", "Logo", "Name", "Price", "Rating", "TotalRating" },
                values: new object[] { "https://c4.wallpaperflare.com/wallpaper/639/471/719/the-witcher-the-witcher-3-wild-hunt-geralt-of-rivia-sword-wallpaper-preview.jpg", 80, new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "RPG", "https://upload.wikimedia.org/wikipedia/fr/4/44/The_Witcher_3_Wild_Hunt_Logo.png", "The Witcher 3: Wild Hunt", 39.99m, 18, 92 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Background", "Count", "DateCreated", "Genre", "Logo", "Name", "Price", "Rating", "TotalRating" },
                values: new object[] { "https://thumbs.dreamstime.com/b/twisted-woodland-scenery-resembling-minecraft-landscape-ai-generative-design-background-instagram-facebook-wall-painting-329315903.jpg", 500, new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sandbox", "https://thumbs.dreamstime.com/b/minecraft-logo-online-game-dirt-block-illustrations-concept-design-isolated-186775550.jpg", "Minecraft", 26.95m, 0, 95 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Background", "Count", "DateCreated", "Genre", "Logo", "Name", "Rating", "TotalRating" },
                values: new object[] { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbKRnhoxCDNKrFWG11mQvGnkAgk0OozaeQ8Q&s", 120, new DateTime(2020, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulation", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdfEjpNy40itgijgBOLfOHvsRggrO9ViqoDQ&s", "Animal Crossing: New Horizons", 0, 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Background", "Count", "DateCreated", "Genre", "Logo", "Name", "Price", "Rating", "TotalRating" },
                values: new object[] { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdqRoYXkrm6WD1Sd4J6VxdEC9GXEA-Mf0Ngw&s", 1000, new DateTime(2020, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action RPG", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0gKGxPvb-I7k6wdByzZ5Y7VzBEFg_Dxc6NA&s", "Genshin Impact", 0.00m, 13, 85 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ba0c5c67-0bc8-4f34-9fa4-1774f7df8dc1"), new Guid("64ee9a6f-2f2f-4a05-8796-c688a8eee9c3") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1bd877b3-2972-4acb-9de9-dba69c94618b"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ba0c5c67-0bc8-4f34-9fa4-1774f7df8dc1"), new Guid("64ee9a6f-2f2f-4a05-8796-c688a8eee9c3") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ba0c5c67-0bc8-4f34-9fa4-1774f7df8dc1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("64ee9a6f-2f2f-4a05-8796-c688a8eee9c3"));

            migrationBuilder.DropColumn(
                name: "Background",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1a68da07-a44f-483a-97b8-1b6e1d447ede"), null, "User", "USER" },
                    { new Guid("1bd4c0f5-99fe-46ef-bc39-f1b3bc97ad00"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountCreationDate", "AddressDelivery", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4cfd4348-a5c9-47ad-860d-6deb7297b827"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "3beff870-f36c-4220-80d9-254005b7910b", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEGh987+UHIq6OuL+G7qlwM+UdoWFBh0so301bvhUbtUXHJn7WCE9S6MYAORyZijKfw==", null, false, null, false, "AdminUser" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Name", "Price", "TotalRating" },
                values: new object[] { new DateTime(2024, 6, 1, 9, 19, 28, 252, DateTimeKind.Local).AddTicks(7101), "Game A", 29.99m, 85 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "Name", "Price", "TotalRating" },
                values: new object[] { new DateTime(2024, 8, 1, 9, 19, 28, 252, DateTimeKind.Local).AddTicks(7186), "Game B", 49.99m, 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "Name", "Price", "TotalRating" },
                values: new object[] { new DateTime(2024, 9, 1, 9, 19, 28, 252, DateTimeKind.Local).AddTicks(7194), "Game C", 39.99m, 75 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "Name", "TotalRating" },
                values: new object[] { new DateTime(2024, 10, 1, 9, 19, 28, 252, DateTimeKind.Local).AddTicks(7201), "Game D", 88 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "Name", "Price", "TotalRating" },
                values: new object[] { new DateTime(2024, 10, 22, 9, 19, 28, 252, DateTimeKind.Local).AddTicks(7209), "Game E", 0.99m, 95 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1bd4c0f5-99fe-46ef-bc39-f1b3bc97ad00"), new Guid("4cfd4348-a5c9-47ad-860d-6deb7297b827") });
        }
    }
}
