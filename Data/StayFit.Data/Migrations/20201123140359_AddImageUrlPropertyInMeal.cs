using Microsoft.EntityFrameworkCore.Migrations;

namespace StayFit.Data.Migrations
{
    public partial class AddImageUrlPropertyInMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Meals");
        }
    }
}
