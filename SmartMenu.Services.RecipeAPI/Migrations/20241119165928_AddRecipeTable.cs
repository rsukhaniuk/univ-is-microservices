using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMenu.Services.RecipeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "CategoryName", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Toasted bread topped with fresh tomatoes, basil, garlic, and a drizzle of olive oil.<br/> A classic Italian appetizer.", null, "https://placehold.co/603x403", "Bruschetta", 7.5 },
                    { 2, "Appetizer", "Fresh mozzarella, tomatoes, and basil, drizzled with balsamic glaze.<br/> A light and refreshing start to any meal.", null, "https://placehold.co/602x402", "Caprese Salad", 9.9900000000000002 },
                    { 3, "Dessert", "A classic Italian dessert with layers of coffee-soaked ladyfingers and mascarpone cheese, dusted with cocoa powder.", null, "https://placehold.co/601x401", "Tiramisu", 10.99 },
                    { 4, "Entree", "Layered pasta with rich Bolognese sauce, creamy béchamel, and melted cheese.<br/> A hearty and satisfying Italian main course.", null, "https://placehold.co/600x400", "Lasagna", 14.5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
