using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Count.DataAccess.Migrations
{
    public partial class AddingCAloriesToMealFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Calories",
                table: "MealFoods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "MealFoods");
        }
    }
}
