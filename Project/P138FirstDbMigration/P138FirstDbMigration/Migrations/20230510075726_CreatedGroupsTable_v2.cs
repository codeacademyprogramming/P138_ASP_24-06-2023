using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P138FirstDbMigration.Migrations
{
    public partial class CreatedGroupsTable_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Groups");
        }
    }
}
