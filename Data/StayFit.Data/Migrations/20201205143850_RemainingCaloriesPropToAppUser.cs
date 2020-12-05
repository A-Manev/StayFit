namespace StayFit.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemainingCaloriesPropToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RemainingCalories",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingCalories",
                table: "AspNetUsers");
        }
    }
}
