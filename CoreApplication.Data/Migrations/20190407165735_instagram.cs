using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApplication.Data.Migrations
{
    public partial class instagram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "inst",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inst",
                table: "AspNetUsers");
        }
    }
}
