using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClosetItemApp.Api.Migrations
{
    public partial class SeededDefaultRolesAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42358d3e-3c22-45e1-be81-6caa7ba865ef", "33f20449-a151-47ec-8772-d1d418fe239c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1b5952a-2162-46c7-b29e-1a2a68922c14", "e818f74c-e289-47dc-8278-527da3bd0e8d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3f4631bd-f907-4409-b416-ba356312e659", 0, "e8f8811a-0182-4aa8-9218-47834cdc3a3b", "user@localhost.com", true, false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAEDnN14zOfFiwjF9tkfimYbXeeqigXC7ijjaMk9ucnveSz2miT4sNZTGo/2Hm/D6JcQ==", null, false, "89528e0f-03fa-4b46-9363-d20b31fddbba", false, "user@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "408aa945-3d84-4421-8342-7269ec64d949", 0, "ab90963d-cf84-4a88-8af6-43d95af39732", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAEAACcQAAAAEEQ+6NDWgTtkdD8LJT6GCCbtxkc/JJgAcLMy2dYWwCcq0LBrHLd7SzoIYjtX7c+F4A==", null, false, "6d8d060e-4328-4f62-b455-015580a64a52", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "42358d3e-3c22-45e1-be81-6caa7ba865ef", "3f4631bd-f907-4409-b416-ba356312e659" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d1b5952a-2162-46c7-b29e-1a2a68922c14", "408aa945-3d84-4421-8342-7269ec64d949" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "42358d3e-3c22-45e1-be81-6caa7ba865ef", "3f4631bd-f907-4409-b416-ba356312e659" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1b5952a-2162-46c7-b29e-1a2a68922c14", "408aa945-3d84-4421-8342-7269ec64d949" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42358d3e-3c22-45e1-be81-6caa7ba865ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1b5952a-2162-46c7-b29e-1a2a68922c14");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949");
        }
    }
}
