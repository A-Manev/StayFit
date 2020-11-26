namespace StayFit.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddWeightLoseGainPropToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeightLoseGain",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightLoseGain",
                table: "AspNetUsers");
        }
    }
}
