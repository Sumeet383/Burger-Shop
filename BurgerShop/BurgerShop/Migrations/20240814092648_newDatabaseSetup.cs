using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerShop.Migrations
{
    /// <inheritdoc />
    public partial class newDatabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BurgerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasePrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "BasePrice", "BurgerName", "Description", "ImageUrl" },
                values: new object[,]
                {
                    { 1, 100, "Crispy Supreme", "A serving of the Crispy Chicken Supreme Burger from Betty’s Burgers clocks in at a hearty 865 calories. That’s quite a culinary adventure! 🌟", "~/images/burger_image.png" },
                    { 2, 100, "Surprise", "The legendary surprise burger—a delightful concoction that combines nostalgia, creativity, and a touch of culinary magic! 🍔✨", "~/images/burger_image.png" },
                    { 3, 100, "Whopper", "The Whopper—a legendary burger that has graced fast-food menus since 1957. 🍔", "~/images/burger_image.png" },
                    { 4, 100, "Chilli Cheese", "The world of Chili Cheese Burgers—a delightful fusion of juicy burgers, melty cheese, and a kick of chili goodness. 🍔🌶️", "~/images/burger_image.png" },
                    { 5, 100, "Tandoor Grill", "The flavorful world of Tandoori Chicken Burgers—a delightful twist on traditional burgers that brings together bold spices, juicy chicken, and a touch of South Asian flair. 🍔🌶️", "~/images/burger_image.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "MobileNo", "Password" },
                values: new object[,]
                {
                    { 1, "1234567890", "" },
                    { 2, "9876543210", "" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BurgerCategory", "BurgerType", "Price", "Quantity", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 7, "Veg", "Whopper", 100m, 1, 100m, 2 },
                    { 8, "Non-Veg", "Tandoor Grill", 200m, 1, 200m, 1 },
                    { 9, "Veg", "Crispy Burger", 100m, 2, 200m, 1 }
                });
        }
    }
}
