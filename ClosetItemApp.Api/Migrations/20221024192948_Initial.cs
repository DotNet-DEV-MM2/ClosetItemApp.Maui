using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClosetItemApp.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClosetItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: false),
                    ItemType = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosetItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 1, "Red", "Bottom", "Silk pants" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 2, "Blue", "Bottom", "Jeans" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 3, "Green", "Top", "Silk blouse" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 4, "Brown", "Outerwear", "Fur coat" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 5, "Brown", "Outerwear", "Leather jacket" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 6, "White", "Accessory", "Pearl Necklace" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 7, "Pink", "Top", "Sweatshirt" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 8, "Blue", "Bottom", "Straightleg jeans" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 9, "Black", "Footwear", "High Heels" });

            migrationBuilder.InsertData(
                table: "ClosetItems",
                columns: new[] { "Id", "Color", "ItemType", "ShortName" },
                values: new object[] { 10, "Red/White", "Dress", "Polka-dot dress" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosetItems");
        }
    }
}
