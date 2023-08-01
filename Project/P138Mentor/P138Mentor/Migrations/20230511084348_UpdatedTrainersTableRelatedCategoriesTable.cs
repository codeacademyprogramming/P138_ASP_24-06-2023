using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P138Mentor.Migrations
{
    public partial class UpdatedTrainersTableRelatedCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Trainers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_CategoryId",
                table: "Trainers",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Categories_CategoryId",
                table: "Trainers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Categories_CategoryId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_CategoryId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Trainers");
        }
    }
}
