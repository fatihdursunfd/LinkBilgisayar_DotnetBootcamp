using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment.Data.Migrations
{
    public partial class DescriptionAddedToUserFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserFiles",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserFiles");

        }
    }
}
