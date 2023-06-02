using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Count.DataAccess.Migrations
{
    public partial class AddingGoalWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GoalWeight",
                table: "AspNetUsers",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalWeight",
                table: "AspNetUsers");
        }
    }
}
