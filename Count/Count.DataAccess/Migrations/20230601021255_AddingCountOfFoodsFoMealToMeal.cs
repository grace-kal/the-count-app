using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Count.DataAccess.Migrations
{
    public partial class AddingCountOfFoodsFoMealToMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountOfFoodsForMeal",
                table: "Meals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfFoodsForMeal",
                table: "Meals");
        }
    }
}
