using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApplication.Data.Migrations
{
    public partial class business : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "busCategory",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "busExplain",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "businessPurpose",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "busCategory",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "busExplain",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "businessPurpose",
                table: "AspNetUsers");
        }
    }
}
