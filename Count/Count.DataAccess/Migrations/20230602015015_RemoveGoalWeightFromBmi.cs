using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Count.DataAccess.Migrations
{
    public partial class RemoveGoalWeightFromBmi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalWeight",
                table: "BmisUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GoalWeight",
                table: "BmisUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
