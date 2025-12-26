using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApplication.Data.Migrations
{
    public partial class chessLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "chessLevel",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chessLevel",
                table: "AspNetUsers");
        }
    }
}
