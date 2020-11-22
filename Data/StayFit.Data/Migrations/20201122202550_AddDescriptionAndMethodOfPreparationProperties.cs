namespace StayFit.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddDescriptionAndMethodOfPreparationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MethodOfPreparation",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "MethodOfPreparation",
                table: "Meals");
        }
    }
}
