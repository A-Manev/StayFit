namespace StayFit.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeMealQuantityPropTypeFormDecimalToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealDiary_AspNetUsers_UserId",
                table: "MealDiary");

            migrationBuilder.DropForeignKey(
                name: "FK_MealDiary_Meals_MealId",
                table: "MealDiary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealDiary",
                table: "MealDiary");

            migrationBuilder.RenameTable(
                name: "MealDiary",
                newName: "MealsDiary");

            migrationBuilder.RenameIndex(
                name: "IX_MealDiary_UserId",
                table: "MealsDiary",
                newName: "IX_MealsDiary_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MealDiary_MealId",
                table: "MealsDiary",
                newName: "IX_MealsDiary_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_MealDiary_IsDeleted",
                table: "MealsDiary",
                newName: "IX_MealsDiary_IsDeleted");

            migrationBuilder.AlterColumn<double>(
                name: "MealQuantity",
                table: "MealsDiary",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealsDiary",
                table: "MealsDiary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealsDiary_AspNetUsers_UserId",
                table: "MealsDiary",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealsDiary_Meals_MealId",
                table: "MealsDiary",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealsDiary_AspNetUsers_UserId",
                table: "MealsDiary");

            migrationBuilder.DropForeignKey(
                name: "FK_MealsDiary_Meals_MealId",
                table: "MealsDiary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealsDiary",
                table: "MealsDiary");

            migrationBuilder.RenameTable(
                name: "MealsDiary",
                newName: "MealDiary");

            migrationBuilder.RenameIndex(
                name: "IX_MealsDiary_UserId",
                table: "MealDiary",
                newName: "IX_MealDiary_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MealsDiary_MealId",
                table: "MealDiary",
                newName: "IX_MealDiary_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_MealsDiary_IsDeleted",
                table: "MealDiary",
                newName: "IX_MealDiary_IsDeleted");

            migrationBuilder.AlterColumn<decimal>(
                name: "MealQuantity",
                table: "MealDiary",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealDiary",
                table: "MealDiary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealDiary_AspNetUsers_UserId",
                table: "MealDiary",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealDiary_Meals_MealId",
                table: "MealDiary",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
